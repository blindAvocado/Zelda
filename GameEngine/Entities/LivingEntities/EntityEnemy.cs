using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    [Serializable]
    public class EntityEnemy : LivingEntity
    {
        private static Random random = new Random();

        private int selfDamage;
        private int axis;
        private int moveDirection;
        private int moveTimer;

        public EntityEnemy(Sprite sprite, int life, int moveSpeed, Weapon weapon, int selfDamage) : base(sprite, life, moveSpeed)
        {
            this.SetWeapon(weapon);
            this.selfDamage = selfDamage;

            this.axis = 0;
            this.direction = 0;
            this.moveTimer = GetRandomTimer();
        }

        public int SelfDamage { get { return this.selfDamage; } }

        public override bool OnCollision(Entity other)
        {
            if (other is BlockWall || other is BlockHole)
            {
                return true;
            }
            if (other is EntityDoor)
            {
                return true;
            }

            if (other is EntityPlayer)
            {
                EntityPlayer player = (EntityPlayer)other;
                player.Damage(this.selfDamage);
            }


            return false;
        }

        public override void InitializeLoad()
        {
            //this.SetWeapon(Weapon.None);
            base.InitializeLoad();
        }

        public int GetRandomTimer()
        {
            Random rand = new Random();

            return (rand.Next(10, 30) * 100);
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
            this.moveTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (this.moveTimer >= GetRandomTimer())
            {
                this.moveTimer = 0;
                this.axis = random.Next(2);
                this.moveDirection = random.Next(2);

                if (this.moveDirection == 0)
                    this.moveDirection = -1;

            }

            if (this.moveTimer <= (GetRandomTimer() / 2))
            {
                if (axis == 0)
                    this.Move(this.moveSpeed * this.moveDirection, 0);
                else
                    this.Move(0, this.moveSpeed * this.moveDirection);
            }

            base.UpdateChildren(gameTime, input);
        }
    }
}
