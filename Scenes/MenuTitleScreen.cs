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

        public event Action startGameEvent;
        public event Action loadGameEvent;
        public event Action helpMenuEvent;

        public MenuTitleScreen() : base()
        {
            this.options = new List<string>();
            this.currentOption = 0;
            this.currentOptionY = 150;
            this.selectIcon = new Sprite("select", 75, this.currentOptionY, 2, 1, 0);
            
            this.animationFrame = 0;
            this.animationTimer = 0;

            this.options.Add("start");
            this.options.Add("load");
            this.options.Add("help");
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
                        helpMenuEvent?.Invoke();
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
