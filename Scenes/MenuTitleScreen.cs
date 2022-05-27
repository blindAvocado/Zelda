﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class MenuTitleScreen : MenuBase
    {
        private List<string> options;
        private int currentOption;
        private int currentOptionY;
        private Sprite selectIcon;
        private Game1 game;

        private int animationFrame;
        private int animationTimer;

        public event Action startGameEvent;
        public event Action loadGameEvent;

        public MenuTitleScreen(Game1 game) : base()
        {
            this.options = new List<string>();
            this.currentOption = 0;
            this.currentOptionY = 150;
            this.selectIcon = new Sprite("select", 75, this.currentOptionY, 2, 1, 0);
            this.game = game;
            
            this.animationFrame = 0;
            this.animationTimer = 0;

            this.options.Add("start");
            this.options.Add("load");
            this.options.Add("help");
        }

        public override void Save()
        {
        }

        public void moveSelector(int index)
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

        public void UpdateSpriteAnimation(GameTime gameTime)
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

        public override void Update(GameTime gameTime, Input input)
        {
            this.UpdateSpriteAnimation(gameTime);

            if (input.IsKeyPressed(Keys.S))
            {
                this.moveSelector(1);
            }
            if (input.IsKeyPressed(Keys.W))
            {
                this.moveSelector(-1);
            }
            if (input.IsKeyPressed(Keys.Enter))
            {
                switch (options[currentOption])
                {
                    case "start":
                        startGameEvent?.Invoke();
                        break;
                    case "load":
                        loadGameEvent?.Invoke();
                        break;
                    case "help":
                        game.ChangeScene(new MenuHelp(game));
                        break;
                    default:
                        break;
                }
                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.selectIcon.Draw(spriteBatch);

            spriteBatch.Draw(Resources.Images["logo"], new Vector2(32, 16), Color.White);

            spriteBatch.DrawString(Resources.Fonts["Bold"], "Начать игру", new Vector2(96, 154), Color.Black);
            spriteBatch.DrawString(Resources.Fonts["Bold"], "Загрузить игру", new Vector2(96, 169), Color.Black);
            spriteBatch.DrawString(Resources.Fonts["Bold"], "Помощь", new Vector2(96, 184), Color.Black);
        }
    }
}
