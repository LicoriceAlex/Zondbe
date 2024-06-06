using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AiFirst
{
    internal class AboutState : State
    {
        private Texture2D menuTexture;
        private Texture2D crosshairTexture;
        private Texture2D menuBtn;
        private SpriteFont font;
        private Rectangle menuBtnRect;
        private MouseState mouseState, oldMouseState;
        private Rectangle mouseStateRect;
        internal override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(menuTexture, new Rectangle(0, 0, Data.ScreenWidth, Data.ScreenHeight), Color.Gray);
            spriteBatch.Draw(menuBtn, menuBtnRect, Color.White);
            if (mouseStateRect.Intersects(menuBtnRect))
                spriteBatch.Draw(menuBtn, menuBtnRect, Color.Gray);
            var crosshairPosition = new Vector2(mouseState.X - crosshairTexture.Width / 2, mouseState.Y - crosshairTexture.Height / 2);
            spriteBatch.Draw(crosshairTexture, crosshairPosition, Color.White);

            string description = "Zondbe это динамичный шутер с видом сверху,\n" +
                         "в котором ваша цель заработать наибольшее количество очков,\n" +
                         "убивая зомби и собирая монеты.\n\n\n" +
                         "Особенности:\n\n" +
                         "    Очки: За каждую собранную монету вы получаете 20 очков.\n\n\n" + 
                         "Типы монет:\n\n" +
                         "    Обычная монета: немного увеличивает скорость главного героя.\n" +
                         "    Монета с боеприпасами: увеличивает скорость стрельбы на 5 секунд.\n" +
                         "    Красная монета: замедляет зомби на 5 секунд.\n\n\n" +
                         "Уровень сложности: Со временем скорость движения зомби увеличивается,\n" +
                         "повышая общую сложность игры.\n\n\n" +
                         "Управление:\n\n" +
                         "    Передвижение: W, A, S, D\n" +
                         "    Стрельба: Левая кнопка мыши\n\n\n" +
                         "Погрузитесь в мир Zondbe и постарайтесь выжить как можно дольше,\n" +
                         "набирая максимальное количество очков и одолевая полчища зомби!";

            spriteBatch.DrawString(font, description, new Vector2(100, 100), Color.Red);
        }

        internal override void LoadContent(ContentManager content)
        {
            crosshairTexture = content.Load<Texture2D>("custom_crosshair");
            menuTexture = content.Load<Texture2D>("about_bkg");
            menuBtn = content.Load<Texture2D>("menu_btn");
            font = content.Load<SpriteFont>("AboutFont");
            menuBtnRect = new Rectangle(100, Data.ScreenHeight - 200, menuBtn.Width, menuBtn.Height);
        }

        internal override void Update(GameTime gameTime)
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseStateRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (mouseState.LeftButton == ButtonState.Pressed && mouseStateRect.Intersects(menuBtnRect))
                Data.CurrentScene = Data.Scenes.Menu;
        }
    }
}
