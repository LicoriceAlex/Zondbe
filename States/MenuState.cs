using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AiFirst
{
    internal class MenuState : State
    {
        private const int BTNS_COUNT = 3;
        private Texture2D[] btns = new Texture2D[BTNS_COUNT];
        private Texture2D menuTexture;
        private Texture2D crosshairTexture;
        private Rectangle[] btnsRect = new Rectangle[BTNS_COUNT];
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;
        internal override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(menuTexture, new Rectangle(0, 0, Data.ScreenWidth, Data.ScreenHeight), Color.White);
            for (int i = 0; i < BTNS_COUNT; i++)
            {
                spriteBatch.Draw(btns[i], btnsRect[i], Color.White);
                if (mouseStateRect.Intersects(btnsRect[i]))
                    spriteBatch.Draw(btns[i], btnsRect[i], Color.Gray);
            }
            var crosshairPosition = new Vector2(mouseState.X - crosshairTexture.Width / 2, mouseState.Y - crosshairTexture.Height / 2);
            spriteBatch.Draw(crosshairTexture, crosshairPosition, Color.White);
        }

        internal override void LoadContent(ContentManager content)
        {
            crosshairTexture = content.Load<Texture2D>("custom_crosshair");
            menuTexture = content.Load<Texture2D>("zondbe_menu_text");
            const int INC = 140;
            btns[0] = content.Load<Texture2D>("play_btn");
            btns[1] = content.Load<Texture2D>("about_btn");
            btns[2] = content.Load<Texture2D>("exit_btn");
            for (int i = 0; i < BTNS_COUNT; i++)
            {
                btnsRect[i] = new Rectangle(Data.ScreenWidth - btns[i].Width - 175, 350 + (INC * i), btns[i].Width, btns[i].Height);
            }
        }

        internal override void Update(GameTime gameTime)
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(btnsRect[0]))
            {
                DataMethods.ResetData();
                Data.CurrentScene = Data.Scenes.Game;
            }
                
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(btnsRect[1]))
                Data.CurrentScene = Data.Scenes.About;
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(btnsRect[2]))
                Data.Exit = true;
        }
    }
}
