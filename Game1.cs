using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Zelda
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MenuBase currentMenu;
        private KeyboardState oldState;
        private KeyboardState currentState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = Settings.IS_MOUSE_VISIBLE;
            Content.RootDirectory = "Content";

            MenuTitleScreen.startGameEvent += ChangeScene;
        }

        protected override void Initialize()
        {
            base.Initialize();

            //this.currentMenu = new MenuGame();
            this.currentMenu = new MenuTitleScreen(this);

            graphics.PreferredBackBufferWidth = Settings.SCREEN_WIDTH * (int)Settings.PIXEL_RATIO;
            graphics.PreferredBackBufferHeight = Settings.SCREEN_HEIGHT * (int)Settings.PIXEL_RATIO;
            graphics.IsFullScreen = Settings.IS_FULLSCREEN;
            graphics.ApplyChanges();

            this.oldState = Keyboard.GetState();
            this.currentState = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.LoadImages(this.Content);
            Resources.LoadSounds(this.Content);
            Resources.LoadFonts(this.Content);

        }

        public void ChangeScene(MenuBase scene)
        {
            this.currentMenu = scene;
        }

        public void SaveGame()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            using (FileStream file = new FileStream("save.dat", FileMode.OpenOrCreate))
            {
                //binFormatter.Serialize(file, (MenuGame)this.currentGameMenu);
            }
        }

        public static MenuBase LoadGame()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            using (FileStream file = new FileStream("save.dat", FileMode.OpenOrCreate))
            {
                if (file != null)
                {
                    MenuGame loadedScene = (MenuGame)binFormatter.Deserialize(file);

                    //this.currentMenu = loadedScene;

                    return loadedScene;
                }
                return null;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            this.currentState = Keyboard.GetState();

            if (this.currentState.IsKeyDown(Keys.Escape))
                Exit();

            if (this.currentMenu is MenuGame)
            {
                if (this.currentState.IsKeyDown(Keys.P))
                {
                    this.SaveGame();
                    Console.WriteLine("Saved!");
                }
            }

            this.currentMenu.Update(gameTime, new Input(this.oldState, this.currentState));

            this.oldState = this.currentState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (this.currentMenu is MenuTitleScreen)
            {
                GraphicsDevice.Clear(Color.LightGray);
            }


            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Settings.PIXEL_RATIO));

            this.currentMenu.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
