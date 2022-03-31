using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.currentMenu = new MenuGame();

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

        protected override void Update(GameTime gameTime)
        {
            this.currentState = Keyboard.GetState();

            if (this.currentState.IsKeyDown(Keys.Escape))
                Exit();

            this.currentMenu.Update(gameTime, new Input(this.oldState, this.currentState));

            this.oldState = this.currentState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Settings.PIXEL_RATIO));

            this.currentMenu.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
