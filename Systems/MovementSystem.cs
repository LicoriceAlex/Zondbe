using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AiFirst
{
    public class MovementSystem
    {
        public void Update(Hero hero, List<Enemy> enemies, List<Coin> coins, float deltaTime)
        {
            if (GameSettingsData.ShootSpeedBoostTimer > 0)
            {
                GameSettingsData.ShootSpeedBoostTimer -= deltaTime;
                if (GameSettingsData.ShootSpeedBoostTimer <= 0)
                {
                    GameSettingsData.ShootSpeedMultiplier = 1f;
                }
            }
            if (hero.DamageCooldown.damageCooldown > 0)
            {
                hero.DamageCooldown.damageCooldown -= deltaTime;
            }
            var oldPosition = hero.Position;
            hero.Position.X += hero.Velocity.X * deltaTime * GameSettingsData.HeroSpeedMultiplier;
            hero.Position.Y += hero.Velocity.Y * deltaTime * GameSettingsData.HeroSpeedMultiplier;
            hero.Position.X = MathHelper.Clamp(hero.Position.X, 0, Data.ScreenWidth - hero.Sprite.Texture.Width * (int)TextureSizesData.HeroSizeMultiplier);
            hero.Position.Y = MathHelper.Clamp(hero.Position.Y, 0, Data.ScreenHeight - hero.Sprite.Texture.Height * (int)TextureSizesData.HeroSizeMultiplier);
            RotateHeroTowardsMouse(hero);
            foreach (var enemy in enemies)
            {
                if (CollisionMethods.IsCollision(hero, enemy))
                {
                    hero.Health.health = 0;
                    hero.Position = oldPosition;
                    break;
                }
            }
            for (var i = coins.Count - 1; i >= 0; i--)
            {
                var coin = coins[i];
                if (CollisionMethods.IsCollision(hero, coin))
                {
                    coins.RemoveAt(i);
                    if (coin.CointType.coinType == CoinType.Type.Normal)
                    {
                        Data.Score += 20;
                        Data.CoinsCollected++;
                        GameSettingsData.HeroSpeedMultiplier += GameSettingsData.HeroSpeedIncreaser;
                    }
                    else if (coin.CointType.coinType == CoinType.Type.ShootSpeedBoost)
                    {
                        Data.Score += 20;
                        GameSettingsData.ShootSpeedMultiplier = 2f;
                        GameSettingsData.ShootSpeedBoostTimer = 5f;
                    }
                    else if (coin.CointType.coinType == CoinType.Type.EnemySlow)
                    {
                        Data.Score += 20;
                        GameSettingsData.EnemySlowMultiplier = 0.3f;
                        GameSettingsData.EnemySlowTimer = 5f;
                    }
                }
            }
        }
        private void RotateHeroTowardsMouse(Hero hero)
        {
            var mouseState = Mouse.GetState();
            var heroCenterPosition = new Vector2(hero.Position.X + hero.Sprite.Texture.Width / 2, hero.Position.Y + hero.Sprite.Texture.Height / 2);
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            var direction = mousePosition - heroCenterPosition;
            hero.Rotation = new Rotation { rotation = (float)Math.Atan2(direction.Y, direction.X) };
        }
    }
}