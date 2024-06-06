using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AiFirst
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager gameStateManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            Data.ScreenHeight = 1000;
            Data.ScreenWidth = 1900;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameStateManager = new GameStateManager();
            gameStateManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            gameStateManager.Update(gameTime);

            if (Data.Exit)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin();
            gameStateManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
