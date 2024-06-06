using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AiFirst
{
    public class EnemyMovementSystem
    {
        public void Update(List<Enemy> enemies, Hero hero, float deltaTime)
        {
            if (GameSettingsData.EnemySlowTimer > 0)
            {
                GameSettingsData.EnemySlowTimer -= deltaTime;
                if (GameSettingsData.EnemySlowTimer <= 0)
                {
                    GameSettingsData.EnemySlowMultiplier = 1f;
                }
            }
            foreach (var enemy in enemies)
            {
                var enemyPosition = new Vector2(enemy.Position.X, enemy.Position.Y);
                var heroPosition = new Vector2(hero.Position.X, hero.Position.Y);
                var direction = Vector2.Normalize(heroPosition - enemyPosition);
                enemy.Velocity.X = direction.X * GameSettingsData.EnemyBaseSpeed * GameSettingsData.EnemySpeedMultiplier * GameSettingsData.EnemySlowMultiplier;
                enemy.Velocity.Y = direction.Y * GameSettingsData.EnemyBaseSpeed * GameSettingsData.EnemySpeedMultiplier * GameSettingsData.EnemySlowMultiplier;
                var oldPosition = enemy.Position;
                enemy.Position.X += enemy.Velocity.X * deltaTime;
                enemy.Position.Y += enemy.Velocity.Y * deltaTime;
                enemy.Rotation = new Rotation { rotation = (float)Math.Atan2(direction.Y, direction.X) };
                if (CollisionMethods.IsCollision(hero, enemy))
                {
                    hero.Health.health = 0;
                    enemy.Position = oldPosition;
                    break;
                }
            }
        }
    }
}
