using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class MenuDeath : MenuBase
    {
        public static event Action restartGameEvent;
        public static event Action mainMenuEvent;

        public MenuDeath() : base()
        {
            this.options = new List<string>();
            this.currentOption = 0;
            this.currentOptionY = 113;
            this.selectIcon = new Sprite("select", 75, this.currentOptionY, 2, 1, 0);

            this.animationFrame = 0;
            this.animationTimer = 0;

            this.options.Add("restart");
            this.options.Add("menu");

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
                    case "restart":
                        restartGameEvent?.Invoke();
                        break;
                    case "menu":
                        mainMenuEvent?.Invoke();
                        break;
                    default:
                        break;
                }

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.selectIcon.Draw(spriteBatch);

            spriteBatch.DrawString(Resources.Fonts["Bold"], "Вы погибли", new Vector2(Settings.SCREEN_WIDTH / 2 - 40, Settings.SCREEN_HEIGHT / 2 - 20), Color.GhostWhite);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "Начать заново", new Vector2(100, 115), Color.GhostWhite);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "Выйти в меню", new Vector2(100, 130), Color.GhostWhite);
        }
    }
}
