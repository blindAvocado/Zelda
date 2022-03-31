﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public abstract class MenuBase
    {

        protected MenuBase()
        {

        }

        public abstract void Update(GameTime gameTime, Input input);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
