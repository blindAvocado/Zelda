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
        //protected weapon;

        protected Direction direction;

        protected int animationFrame;
        protected int animationTimer;


        protected LivingEntity(Sprite sprite, int maxLife = 1, int moveSpeed = 1, int x = 0, int y = 0) : base(sprite, x, y)
        {
            this.currentLife = maxLife;
            this.maxLife = maxLife;
            this.moveSpeed = moveSpeed;
            this.direction = Direction.DOWN;
            this.animationFrame = 0;
            this.animationTimer = 0;
        }

        public int CurrentLife { get { return this.currentLife; } }
        public int MaxLife { get { return this.maxLife; } }

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

        public void UpdateSpriteAnimation()
        {
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
            this.UpdateSpriteAnimation();
        }
    }
}
