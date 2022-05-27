using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zelda
{
    public class MenuHelp : MenuBase
    {
        private Game1 game;

        public MenuHelp(Game1 game) : base()
        {
            this.game = game;
        }

        public override void Save()
        {
        }

        public override void Update(GameTime gameTime, Input input)
        {
            if (input.IsKeyPressed(Keys.Escape))
            {
                this.game.ChangeScene(game.GetMainMenu);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Resources.Fonts["Bold"], "Помощь", new Vector2(Settings.SCREEN_WIDTH / 2 - 20, 16), Color.GhostWhite);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "Игра является представителем жанра рогалик\n(Rogue-like). " +
                                                              "Игры такого жанра нельзя 'пройти'.\nИгрок устанавливает цель сам.\n" +
                                                              "Уровни в подземелье генерируются случайно.",
                                   new Vector2(10, 30), Color.GhostWhite);
            //W
            new Sprite("keyboard", 10, 80, 8, 7, 6, 4).Draw(spriteBatch);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "вперед", new Vector2(30, 81), Color.GhostWhite);
            //A
            new Sprite("keyboard", 10, 95, 8, 7, 0, 2).Draw(spriteBatch);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "влево", new Vector2(30, 96), Color.GhostWhite);
            //S
            new Sprite("keyboard", 10, 110, 8, 7, 2, 4).Draw(spriteBatch);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "назад", new Vector2(30, 111), Color.GhostWhite);
            //D
            new Sprite("keyboard", 10, 125, 8, 7, 3, 2).Draw(spriteBatch);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "вправо", new Vector2(30, 126), Color.GhostWhite);
            //N
            new Sprite("keyboard", 10, 140, 8, 7, 5, 3).Draw(spriteBatch);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "оружие A", new Vector2(30, 141), Color.GhostWhite);
            //M
            new Sprite("keyboard", 10, 155, 8, 7, 4, 3).Draw(spriteBatch);
            spriteBatch.DrawString(Resources.Fonts["Normal"], "оружие B", new Vector2(30, 156), Color.GhostWhite);
        }
    }
}
