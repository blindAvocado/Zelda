using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public abstract class Entity
    {
        private Rectangle hitbox;
        protected Sprite sprite;
        protected bool hasMoved;

        public void SetPosition(int x, int y)
        {
            this.hitbox.X = x;
            this.hitbox.Y = y;
        }

        public Entity(Sprite sprite, int x, int y)
        {
            this.sprite = sprite;
            this.hitbox = new Rectangle(x, y, this.sprite.Width, this.sprite.Height);
            this.hasMoved = false;
        }

        public bool HasMoved { get { return this.hasMoved; } }

        public virtual void Move(int offsetX, int offsetY)
        {
            this.hitbox.X += offsetX;
            this.hitbox.Y += offsetY;
            this.hasMoved = true;
        }
        public virtual void Move(Point point)
        {
            this.Move(point.X, point.Y);
        }

        public void CancelMove(Vector2 intersectionDepth)
        {
            int x = (int)intersectionDepth.X;
            int y = (int)intersectionDepth.Y;


            if (Math.Abs(x) > Math.Abs(y))
                this.hitbox.Y += y;
            else
                this.hitbox.X += x;
        }

        public CollisionInfo CollisionWith(Entity other)
        {
            return new CollisionInfo(this.hitbox.GetIntersectionDepth(other.hitbox));
        }

        public virtual bool OnCollision(Entity other)
        {
            return false;
        }

        public virtual void Update(GameTime gameTime, Input input)
        {
            this.hasMoved = false;
            this.UpdateChildren(gameTime, input);
        }

        public void UpdateSprite()
        {
            this.sprite.Update(this.hitbox.X, this.hitbox.Y);
        }

        public abstract void UpdateChildren(GameTime gameTime, Input input);

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }
    }
}
