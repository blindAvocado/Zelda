using Microsoft.Xna.Framework;
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
        private Sprite logo;
        private Sprite selectIcon;
        private Game1 game;

        private int animationFrame;
        private int animationTimer;

        public MenuTitleScreen(Game1 game) : base(game)
        {
            this.options = new List<string>();
            this.currentOption = 0;
            this.currentOptionY = 150;
            this.logo = new Sprite("logo", 32, 16);
            this.selectIcon = new Sprite("select", 75, this.currentOptionY, 2, 1, 0);
            this.game = game;
            
            this.animationFrame = 0;
            this.animationTimer = 0;

            this.options.Add("start");
            this.options.Add("load");
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
                        Console.WriteLine("START");
                        game.ChangeScene(new MenuGame(game));
                        break;
                    case "load":
                        Console.WriteLine("LOAD");
                        game.LoadGame();
                        break;
                    default:
                        break;
                }
                
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.logo.Draw(spriteBatch);
            this.selectIcon.Draw(spriteBatch);

            spriteBatch.DrawString(Resources.Fonts["Font"], "Start Game", new Vector2(96, 154), Color.Black);
            spriteBatch.DrawString(Resources.Fonts["Font"], "Load Game", new Vector2(96, 169), Color.Black);
        }
    }
}
