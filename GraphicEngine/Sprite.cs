using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class Sprite
    {
        protected Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;
        private Color color;
        private float rotation;
        private Vector2 origin;
        private SpriteEffects effects;

        private int spriteWidth;
        private int spriteHeight;
        private Point currentFrame;


        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetCurrentFrame(int x, int y)
        {
            this.currentFrame.X = x;
            this.currentFrame.Y = y;
            this.UpdateCurrentFrame();

        }

        public Sprite(string imageKey, int x, int y, int columns = 1, int rows = 1, int frameX = 0, int frameY = 0)
        {
            this.texture = Resources.Images[imageKey];

            int textureWidth = this.texture.Width;
            int textureHeight = this.texture.Height;

            this.spriteWidth = textureWidth / columns;
            this.spriteHeight = textureHeight / rows;

            this.destinationRectangle = new Rectangle(x, y, this.spriteWidth, this.spriteHeight);

            this.currentFrame = new Point(frameX, frameY);

            this.sourceRectangle = new Rectangle(this.currentFrame.X * this.spriteWidth,
                                     this.currentFrame.Y * this.spriteHeight,
                                     this.spriteWidth,
                                     this.spriteHeight);

            this.color = Color.White;
            this.rotation = 0f;
            this.origin = Vector2.Zero;
            this.effects = SpriteEffects.None;
        }

        public int Width { get { return this.spriteWidth; }  }
        public int Height{ get { return this.spriteHeight; }  }

        public void UpdateCurrentFrame()
        {
            this.sourceRectangle.X = this.currentFrame.X * this.spriteWidth;
            this.sourceRectangle.Y = this.currentFrame.Y * this.spriteHeight;

        }

        public void UpdatePosition(int x, int y)
        {
            this.destinationRectangle.X = x;
            this.destinationRectangle.Y = y;
        }

        public void Update(int x, int y)
        {
            this.UpdatePosition(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.destinationRectangle, this.sourceRectangle, this.color, this.rotation, this.origin, this.effects, 0f);
        }
    }
}
