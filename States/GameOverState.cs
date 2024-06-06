using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AiFirst
{
    internal class GameOverState : State
    {
        private const int BTNS_COUNT = 3;
        private Texture2D[] btns = new Texture2D[BTNS_COUNT];
        private Texture2D gameOverTexture;
        private Texture2D crosshairTexture;
        private SpriteFont font;
        private Rectangle[] btnsRect = new Rectangle[BTNS_COUNT];
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOverTexture, new Rectangle(0, 0, Data.ScreenWidth, Data.ScreenHeight), Color.Gray);
            for (int i = 0; i < BTNS_COUNT; i++)
            {
                spriteBatch.Draw(btns[i], btnsRect[i], Color.White);
                if (mouseStateRect.Intersects(btnsRect[i]))
                    spriteBatch.Draw(btns[i], btnsRect[i], Color.Gray);
                var crosshairPosition = new Vector2(mouseState.X - crosshairTexture.Width / 2, mouseState.Y - crosshairTexture.Height / 2);
                spriteBatch.Draw(crosshairTexture, crosshairPosition, Color.White);
            }
            var scoreText = $"Счет: {Data.Score}";
            var coinsText = $"Собрано монет: {Data.CoinsCollected}";
            spriteBatch.DrawString(font, scoreText, new Vector2(Data.ScreenWidth / 2 - btns[0].Width / 2, Data.ScreenHeight / 2 - 150), Color.Red);
            spriteBatch.DrawString(font, coinsText, new Vector2(Data.ScreenWidth / 2 - btns[0].Width / 2, Data.ScreenHeight / 2 - 100), Color.Red);
        }

        internal override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("gameFont");
            crosshairTexture = content.Load<Texture2D>("custom_crosshair");
            gameOverTexture = content.Load<Texture2D>("asphalt");
            btns[0] = content.Load<Texture2D>("retry_btn");
            btns[1] = content.Load<Texture2D>("menu_btn");
            btns[2] = content.Load<Texture2D>("exit_btn");
            const int INC = 140;
            for (int i = 0; i < BTNS_COUNT; i++)
            {
                btnsRect[i] = new Rectangle((Data.ScreenWidth / 2) - (btns[i].Width / 2), 500 + (INC * i), btns[i].Width, btns[i].Height);
            }
        }

        internal override void Update(GameTime gameTime)
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(btnsRect[0]))
            {
                Data.CurrentScene = Data.Scenes.Game;
                DataMethods.ResetData();
            }
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(btnsRect[1]))
                Data.CurrentScene = Data.Scenes.Menu;
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(btnsRect[2]))
                Data.Exit = true;
        }
        
    }
}
