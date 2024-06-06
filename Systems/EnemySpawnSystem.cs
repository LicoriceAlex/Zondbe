using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AiFirst
{
    public class EnemySpawnSystem
    {
        private ContentManager _content;
        private Texture2D _enemyTexture;
        private float _timeSinceLastSpawn = 0f;

        public EnemySpawnSystem(ContentManager content)
        {
            _content = content;
            _enemyTexture = _content.Load<Texture2D>("zondbe");
        }

        public void Update(List<Enemy> enemies, Hero hero, GameTime gameTime)
        {
            _timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timeSinceLastSpawn >= GameSettingsData.EnemySpawnInterval)
            {
                _timeSinceLastSpawn = 0f;

                var random = new System.Random();
                var textureWidth = _enemyTexture.Width * (int)TextureSizesData.EnemySizeMultiplier;
                var textureHeight = _enemyTexture.Height * (int)TextureSizesData.EnemySizeMultiplier;
                float x, y;
                do
                {
                    x = random.Next(textureWidth, Data.ScreenWidth - textureWidth);
                    y = random.Next(textureHeight, Data.ScreenHeight - textureHeight);
                } while (Vector2.DistanceSquared(new Vector2(x, y), new Vector2(hero.Position.X, hero.Position.Y)) < GameSettingsData.EnemyMinSpawnDistance * GameSettingsData.EnemyMinSpawnDistance);

                var enemy = new Enemy
                {
                    Sprite = new Sprite { Texture = _enemyTexture, SizeMultiplier = TextureSizesData.EnemySizeMultiplier },
                    Position = new Position { X = x, Y = y },
                    Velocity = new Velocity { X = 0, Y = 0 },
                    Health = new Health { health = GameSettingsData.EnemyHealth },
                    Damage = new Damage { damage = GameSettingsData.EnemyDamage }
                };
                enemies.Add(enemy);
            }
        }
    }
}
