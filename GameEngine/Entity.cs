using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public abstract class Entity
    {
        protected Room currentRoom;
        private Rectangle position;
        private Rectangle baseHitbox;
        private Rectangle hitbox;
        protected Sprite sprite;
        protected bool hasMoved;

        private bool isDestroyed;

        public void SetRoom(Room room)
        {
            this.currentRoom = room;
        }

        public void SetPosition(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
            this.UpdateHitbox();
        }

        protected Entity(Sprite sprite, int x, int y)
        {
            this.Initialize(sprite, x, y, new Rectangle(0, 0, sprite.Width, sprite.Height));
        }


        protected Entity(Sprite sprite, int x, int y, Rectangle baseHitbox)
        {
            this.Initialize(sprite, x, y, baseHitbox);
        }

        private void Initialize(Sprite sprite, int x, int y, Rectangle baseHitbox)
        {
            this.currentRoom = null;
            this.sprite = sprite;
            this.position = new Rectangle(x, y, this.sprite.Width, this.sprite.Height);


            this.baseHitbox = baseHitbox;
            this.hitbox = new Rectangle(this.position.X + this.baseHitbox.X,
                                        this.position.Y + this.baseHitbox.Y,
                                        this.baseHitbox.Width,
                                        this.baseHitbox.Height);
            this.hasMoved = false;
            this.isDestroyed = false;
        }

        public bool HasMoved { get { return this.hasMoved; } }
        public bool IsDestroyed { get { return this.isDestroyed;  } }
        public int RoomX
        {
            get
            {
                return (this.hitbox.Center.X - Room.ROOM_OFFSET_X) / EntityBlock.WIDTH;
            }
        }
        public int RoomY
        {
            get
            {
                return (this.hitbox.Center.Y - Room.ROOM_OFFSET_Y) / EntityBlock.HEIGHT;
            }
        }

        public Rectangle Hitbox { get { return this.hitbox; } }

        public virtual void Move(int offsetX, int offsetY)
        {
            this.position.X += offsetX;
            this.position.Y += offsetY;
            this.hasMoved = true;
            this.UpdateHitbox();
        }
        public void Move(Point point)
        {
            this.Move(point.X, point.Y);
        }

        public void Destroy()
        {
            this.isDestroyed = true;
        }

        public void CancelMove(Vector2 intersectionDepth)
        {
            int x = (int)intersectionDepth.X;
            int y = (int)intersectionDepth.Y;

            Console.WriteLine(this + " " + intersectionDepth.X.ToString() + " " + intersectionDepth.Y.ToString());

            if (Math.Abs(x) > Math.Abs(y))
                this.position.Y += y;
            else
                this.position.X += x;
            this.UpdateHitbox();
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
            this.UpdateHitbox();
        }

        public void UpdateHitbox()
        {
            this.hitbox.X = this.position.X + this.baseHitbox.X;
            this.hitbox.Y = this.position.Y + this.baseHitbox.Y;
        }

        public void UpdateSprite()
        {
            this.sprite.Update(this.position.X, this.position.Y);
        }

        public abstract void UpdateChildren(GameTime gameTime, Input input);

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }
    }
}
