using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public enum Direction
    {
        UP,
        LEFT,
        RIGHT,
        DOWN
    }

    public abstract class LivingEntity : Entity
    {
        protected int currentLife;
        protected int maxLife;
        protected int moveSpeed;
        protected Weapon weapon;

        protected Direction direction;

        protected int animationFrame;
        protected int animationTimer;

        public void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
            weapon.SetOwner(this);
        }

        protected LivingEntity(Sprite sprite, int maxLife = 1, int moveSpeed = 1, int x = 0, int y = 0) : base(sprite, x, y)
        {
            this.sprite.SetLayerDepth(0.3f);
            this.currentRoom = null;
            this.currentLife = maxLife;
            this.maxLife = maxLife;
            this.moveSpeed = moveSpeed;
            this.weapon = Weapon.None;
            this.direction = Direction.DOWN;
            this.animationFrame = 0;
            this.animationTimer = 0;
        }

        public int CurrentLife { get { return this.currentLife; } }
        public int MaxLife { get { return this.maxLife; } }
        public Weapon Weapon { get { return this.weapon; } }

        public void Damage(int damage)
        {
            this.currentLife -= damage;

            if (this.currentLife <= 0)
            {
                this.Destroy();
            }
        }

        public void Heal(int amount)
        {
            if (this.currentLife + amount <= this.maxLife)
            {
                this.currentLife += amount;
            }
            else
            {
                this.currentLife = this.maxLife;
            }
        }

        public void UseWeapon()
        {
            if (this.weapon != Weapon.None && this.currentRoom != null)
            {
                if (this.weapon.CanUseWeapon())
                {
                    this.weapon.UseWeapon(this.currentRoom, this.Hitbox, this.direction);
                    this.animationFrame = 2;
                    this.animationTimer = 0;
                }
            }
        }

        public override void Move(int offsetX, int offsetY)
        {
            if (!this.hasMoved)
                this.animationTimer += 16;
                
            base.Move(offsetX, offsetY);

            if (offsetY < 0)
                this.direction = Direction.UP;
            if (offsetX < 0)
                this.direction = Direction.LEFT;
            if (offsetY > 0)
                this.direction = Direction.DOWN;
            if (offsetX > 0)
                this.direction = Direction.RIGHT;

        }

        public void UpdateSpriteAnimation(GameTime gameTime)
        {
            if (this.animationFrame == 2)
            {
                this.animationTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (this.animationTimer >= this.Weapon.AttackSpeed)
                {
                    this.animationFrame = 0;
                    this.animationTimer = 0;
                }
            }

            if (this.animationTimer >= 250)
            {
                this.animationTimer = 0;
                if (this.animationFrame == 0)
                    this.animationFrame = 1;
                else
                    this.animationFrame = 0;
            }


            switch (this.direction)
            {
                case Direction.UP:
                    this.sprite.SetCurrentFrame(2, this.animationFrame);
                    break;
                case Direction.LEFT:
                    this.sprite.SetCurrentFrame(1, this.animationFrame);
                    break;
                case Direction.DOWN:
                    this.sprite.SetCurrentFrame(0, this.animationFrame);
                    break;
                case Direction.RIGHT:
                    this.sprite.SetCurrentFrame(3, this.animationFrame);
                    break;
                default:
                    break;
            }
        }

        public override void UpdateChildren(GameTime gameTime, Input input)
        {
            this.weapon.Update(gameTime);
            this.UpdateSpriteAnimation(gameTime);
        }
    }
}
