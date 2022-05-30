using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    [Serializable]
    public abstract class MenuBase
    {
        protected List<string> options;
        protected int currentOption;
        protected int currentOptionY;
        [NonSerialized] protected Sprite selectIcon;

        protected int animationFrame;
        protected int animationTimer;

        protected MenuBase()
        {

        }

        public abstract void Update(GameTime gameTime, Input input);
        public abstract void Draw(SpriteBatch spriteBatch);
        public virtual void UpdateSpriteAnimation(GameTime gameTime)
        {
            this.animationTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.animationTimer >= 250)
            {
                this.animationTimer = 0;
                if (this.animationFrame == 0)
                    this.animationFrame = 1;
                else
                    this.animationFrame = 0;

            }

            this.selectIcon.SetCurrentFrame(this.animationFrame, 0);
        }

        public virtual void moveSelector(int index)
        {
            if (index > 0)
            {
                if (this.currentOption < this.options.Count - 1)
                    this.currentOption += index;
            }
            else
            {
                if (this.currentOption > 0)
                    this.currentOption += index;
            }

            this.selectIcon.UpdatePosition(75, this.currentOptionY + (this.currentOption * 15));
        }
    }
}
