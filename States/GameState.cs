using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace AiFirst
{
    internal class GameState : State
    {
        private Hero hero;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Coin> coins = new List<Coin>();
        private MovementSystem movementSystem = new MovementSystem();
        private InputSystem inputSystem;
        private BulletSystem bulletSystem = new BulletSystem();
        private Texture2D crosshairTexture;
        private Texture2D backgroundTexture;
        private SpriteFont font;
        private EnemySpawnSystem enemySpawnSystem;
        private EnemyMovementSystem enemyMovementSystem = new EnemyMovementSystem();
        private CoinSpawnSystem coinSpawnSystem;


       

        internal override void LoadContent(ContentManager content)
        {
            crosshairTexture = content.Load<Texture2D>("custom_crosshair");
            var heroTexture = content.Load<Texture2D>("player_handgun");
            backgroundTexture = content.Load<Texture2D>("asphalt");
            font = content.Load<SpriteFont>("gameFont");
            hero = new Hero
            {
                Health = new Health { health = GameSettingsData.HeroHealth },
                Sprite = new Sprite { Texture = heroTexture, SizeMultiplier = TextureSizesData.HeroSizeMultiplier },
                Position = new Position { X = 200, Y = 200 },
                Velocity = new Velocity { X = 0, Y = 0 },
                Input = new PlayerInput { Up = Keys.W, Down = Keys.S, Left = Keys.A, Right = Keys.D }
            };

            inputSystem = new InputSystem(content);
            enemySpawnSystem = new EnemySpawnSystem(content);
            coinSpawnSystem = new CoinSpawnSystem(content);
        }

        internal override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var mouseState = Mouse.GetState();
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);

            inputSystem.Update(hero, deltaTime);
            inputSystem.Shoot(hero, mousePosition);
            movementSystem.Update(hero, enemies, coins, deltaTime);
            coinSpawnSystem.Update(coins, deltaTime);
            bulletSystem.Update(hero.Bullets, enemies, deltaTime);
            enemySpawnSystem.Update(enemies, hero, gameTime);
            enemyMovementSystem.Update(enemies, hero, deltaTime);

            if (hero.Health.health <= 0)
                Data.CurrentScene = Data.Scenes.GameOver;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Data.CurrentScene = Data.Scenes.Pause;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Data.ScreenWidth, Data.ScreenHeight), Color.White);
            var scoreText = $"Счет: {Data.Score} ";
            var coinsText = $"Собрано монет: {Data.CoinsCollected}";
            spriteBatch.DrawString(font, scoreText, new Vector2(5, 5), Color.Red);
            spriteBatch.DrawString(font, coinsText, new Vector2(5, 50), Color.Red);
            foreach (var coin in coins)
            {
                spriteBatch.Draw(coin.Sprite.Texture,
                    new Vector2(coin.Position.X, coin.Position.Y),
                    null, Color.White, 0f, Vector2.Zero,
                    coin.Sprite.SizeMultiplier, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(
                hero.Sprite.Texture,
                new Vector2(hero.Position.X + hero.Sprite.Texture.Width / 2, hero.Position.Y + hero.Sprite.Texture.Height / 2),
                null, Color.White, hero.Rotation.rotation,
                new Vector2(hero.Sprite.Texture.Width / 2, hero.Sprite.Texture.Height / 2),
                hero.Sprite.SizeMultiplier, SpriteEffects.None, 0f);

            foreach (var bullet in hero.Bullets)
            {
                spriteBatch.Draw(
                    bullet.Sprite.Texture,
                    new Vector2(bullet.Position.X, bullet.Position.Y),
                    null, Color.White, 0f,
                    new Vector2(bullet.Sprite.Texture.Width / 2, bullet.Sprite.Texture.Height / 2),
                    bullet.Sprite.SizeMultiplier, SpriteEffects.None, 0f);
            }

            foreach (var enemy in enemies)
            {
                spriteBatch.Draw(
                    enemy.Sprite.Texture,
                    new Vector2(enemy.Position.X + enemy.Sprite.Texture.Width / 2, enemy.Position.Y + enemy.Sprite.Texture.Height / 2),
                    null, Color.White, enemy.Rotation.rotation,
                    new Vector2(enemy.Sprite.Texture.Width / 2, enemy.Sprite.Texture.Height / 2),
                    enemy.Sprite.SizeMultiplier, SpriteEffects.None, 0f);
            }

            var mouseState = Mouse.GetState();
            var crosshairPosition = new Vector2(mouseState.X - crosshairTexture.Width / 2, mouseState.Y - crosshairTexture.Height / 2);
            spriteBatch.Draw(crosshairTexture, crosshairPosition, Color.White);
        }
    }
}
