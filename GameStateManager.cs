using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AiFirst.States;

namespace AiFirst
{
    internal class GameStateManager : State
    {
        private ContentManager _content;
        private MenuState menuState = new MenuState();
        private GameState gameState = new GameState();
        private GameOverState gameOverState = new GameOverState();
        private PauseState pauseState = new PauseState();
        private AboutState aboutState = new AboutState();
        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (Data.CurrentScene)
            {
                case Data.Scenes.Menu:
                    menuState.Draw(spriteBatch); break;
                case Data.Scenes.Game:
                    gameState.Draw(spriteBatch); break;
                case Data.Scenes.GameOver:
                    gameOverState.Draw(spriteBatch); break;
                case Data.Scenes.Pause:
                    pauseState.Draw(spriteBatch); break;
                case Data.Scenes.About:
                    aboutState.Draw(spriteBatch); break;
            }
        }

        internal override void LoadContent(ContentManager content)
        {
            _content = content;
            menuState.LoadContent(content);
            gameState.LoadContent(content);
            gameOverState.LoadContent(content);
            pauseState.LoadContent(content);
            aboutState.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            switch (Data.CurrentScene)
            {
                case Data.Scenes.Menu:
                    menuState.Update(gameTime); break;
                case Data.Scenes.Game:
                    gameState.Update(gameTime); break;
                case Data.Scenes.GameOver:
                    gameOverState.Update(gameTime); break;
                case Data.Scenes.Pause:
                    pauseState.Update(gameTime); break;
                case Data.Scenes.About:
                    aboutState.Update(gameTime); break;
            }

            if (Data.CurrentScene == Data.Scenes.GameOver || Data.CurrentScene == Data.Scenes.Menu)
            {
                gameState = new GameState();
                gameState.LoadContent(_content);
            }
        }
    }
}
