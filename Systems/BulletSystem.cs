using System.Collections.Generic;
using System.Linq;

namespace AiFirst
{
    public class BulletSystem
    {
        public void Update(List<Bullet> bullets, List<Enemy> enemies, float deltaTime)
        {
            for (var i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                bullet.Position.X += bullet.Velocity.X * deltaTime;
                bullet.Position.Y += bullet.Velocity.Y * deltaTime;
                if (bullet.Position.X < 0 || bullet.Position.X > Data.ScreenWidth || bullet.Position.Y < 0 || bullet.Position.Y > Data.ScreenHeight)
                {
                    bullets.RemoveAt(i);
                    continue;
                }

                for (var j = enemies.Count - 1; j >= 0; j--)
                {
                    var enemy = enemies[j];
                    if (CollisionMethods.IsCollision(bullet, enemy))
                    {
                        if (bullets.Count() > 0)
                        {
                            bullets.RemoveAt(i);
                        }
                        enemy.Health.health -= bullet.Damage.damage;
                        if (enemy.Health.health <= 0)
                        {
                            enemies.RemoveAt(j);
                            Data.Score += 20;
                            if (Data.Score % 50 == 0)
                            {
                                GameSettingsData.EnemySpeedMultiplier += GameSettingsData.EnemySpeedIncreaser;
                                GameSettingsData.EnemySpawnInterval *= GameSettingsData.EnemySpawnIntervalMultiplier;
                            }
                        }
                    }
                }
            }
        }
    }
}
