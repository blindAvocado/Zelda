using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    [Serializable]
    public abstract class Entity
    {
        [NonSerialized] protected Rectangle position;
        [NonSerialized] protected Rectangle baseHitbox;
        [NonSerialized] protected Rectangle hitbox;
        [NonSerialized] protected Sprite sprite;
        [NonSerialized] protected bool hasMoved;
        protected string spriteName;
        protected int spriteRow;
        protected int spriteCol;
        protected int[] positionInt = new int[4];
        protected int[] baseHitboxInt = new int[4];
        protected int[] hitboxInt = new int[4];
        protected bool isCreated;
        protected int roomX;
        protected int roomY;

        private bool isDestroyed;

        public void SetPosition(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;

            //positionInt[0] = this.position.X;
            //positionInt[1] = this.position.Y;

            this.UpdateHitbox();
            this.UpdateSprite();
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
            this.sprite = sprite;
            this.position = new Rectangle(x, y, this.sprite.Width, this.sprite.Height);

            this.baseHitbox = baseHitbox;

            this.hitbox = new Rectangle(this.position.X + this.baseHitbox.X,
                                        this.position.Y + this.baseHitbox.Y,
                                        this.baseHitbox.Width,
                                        this.baseHitbox.Height);
            this.hasMoved = false;
            this.isCreated = true;
            this.isDestroyed = false;
        }


        public virtual void InitializeLoad()
        {
            this.baseHitbox = new Rectangle(this.baseHitboxInt[0], this.baseHitboxInt[1], this.baseHitboxInt[2], this.baseHitboxInt[3]);
            this.position = new Rectangle(this.positionInt[0], this.positionInt[1], this.positionInt[2], this.positionInt[3]);
            this.hitbox = new Rectangle(this.hitboxInt[0], this.hitboxInt[1], this.hitboxInt[2], this.hitboxInt[3]);

            this.UpdateHitbox();
            this.UpdateSprite();
        }

        public virtual void Save()
        {

        }

        public bool HasMoved { get { return this.hasMoved; } }
        public bool IsCreated { get { return this.isCreated; } set { this.isCreated = value; } }
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
