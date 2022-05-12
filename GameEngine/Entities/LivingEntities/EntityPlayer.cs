using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class EntityPlayer : LivingEntity
    {
        private int keys;
        private int bombs;
        private int coins;

        public EntityPlayer(int life, int x = 0, int y = 0) : base(new Sprite("link", x, y, 4, 3), life, 1, x, y)
        {
            this.keys = 0;
            this.bombs = 0;
            this.coins = 0;

            //Room.collisionEvent += CancelMove;

        }

        public int KeysCount { get { return this.keys; } }
        public int BombsCount { get { return this.bombs; } }
        public int CoinsCount { get { return this.coins; } }

        public override bool OnCollision(Entity other)
        {
            if (other is ItemEntity)
            {
                ItemEntity item = (ItemEntity)other;
                item.PickupItem(this);
            }

            if (other is BlockWall || other is BlockHole)
                return true;

            if (other is EntityEnemy)
                this.Damage(((EntityEnemy)other).SelfDamage);

            return false;
        }

        public void AddKey(int amount)
        {
            this.keys += amount;
        }
        public void AddBomb(int amount)
        {
            this.bombs += amount;
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
            if (this.animationFrame != 2)
            {
                if (input.IsKeyDown(Keys.W)) // UP
                {
                    this.Move(0, -this.moveSpeed);
                }
                if (input.IsKeyDown(Keys.A)) // LEFT
                {
                    this.Move(-this.moveSpeed, 0);
                }
                if (input.IsKeyDown(Keys.S)) // DOWN
                {
                    this.Move(0, +this.moveSpeed);
                }
                if (input.IsKeyDown(Keys.D)) // RIGHT
                {
                    this.Move(+this.moveSpeed, 0);
                }
            }

            if (input.IsKeyPressed(Keys.N))
                this.UseWeapon();

            base.UpdateChildren(gameTime, input);
        }
    }
}
