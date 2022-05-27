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
        private MenuTitleScreen mainMenu;
        private KeyboardState oldState;
        private KeyboardState currentState;

        public static bool paused = false;

        public MenuBase GetMainMenu { get { return this.mainMenu; } }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = Settings.IS_MOUSE_VISIBLE;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();

            //this.currentMenu = new MenuTitleScreen(this);
            this.mainMenu = new MenuTitleScreen(this);
            this.currentMenu = this.mainMenu;
            //this.currentMenu = new MenuDeath(this);

            this.mainMenu.startGameEvent += StartGame;
            this.mainMenu.loadGameEvent += LoadGame;
            EntityPlayer.playerDeath += ChangeScene;
            MenuDeath.restartGameEvent += StartGame;
            MenuDeath.mainMenuEvent += MainMenu;

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

        private void StartGame()
        {
            this.currentMenu = new MenuGame();
        }

        private void MainMenu()
        {
            this.currentMenu = this.mainMenu;
        }

        public void SaveGame()
        {
            this.currentMenu.Save();

            BinaryFormatter binFormatter = new BinaryFormatter();

            using (FileStream file = new FileStream("save.dat", FileMode.OpenOrCreate))
            {
                binFormatter.Serialize(file, (MenuGame)this.currentMenu);
            }
        }

        public void LoadGame()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            using (FileStream file = new FileStream("save.dat", FileMode.OpenOrCreate))
            {
                if (file != null)
                {
                    MenuGame loadedScene = (MenuGame)binFormatter.Deserialize(file);

                    loadedScene.Initialize();

                    this.ChangeScene(loadedScene);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            this.currentState = Keyboard.GetState();

            //if (this.currentState.IsKeyDown(Keys.Escape))
            //    Exit();

            if (this.currentMenu is MenuGame)
            {
                if (this.currentState.IsKeyDown(Keys.P))
                {
                    this.SaveGame();
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
