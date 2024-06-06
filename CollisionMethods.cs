using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AiFirst
{
    public static class CollisionMethods
    {
        private static RotatedRectangle GetRotatedRectangle(Position position, Texture2D texture, float sizeMultiplier, float rotation)
        {
            var center = new Vector2(position.X + (texture.Width * sizeMultiplier) / 2, position.Y + (texture.Height * sizeMultiplier) / 2);
            return new RotatedRectangle(center, texture.Width * sizeMultiplier, texture.Height * sizeMultiplier, rotation);
        }

        public static bool IsCollision(Hero hero, Enemy enemy)
        {
            var heroRect = GetRotatedRectangle(hero.Position, hero.Sprite.Texture, hero.Sprite.SizeMultiplier, hero.Rotation.rotation);
            var enemyRect = GetRotatedRectangle(enemy.Position, enemy.Sprite.Texture, enemy.Sprite.SizeMultiplier, enemy.Rotation.rotation);
            return SATCollision.IsCollision(heroRect, enemyRect);
        }

        public static bool IsCollision(Hero hero, Coin coin)
        {
            var heroRect = GetRotatedRectangle(hero.Position, hero.Sprite.Texture, hero.Sprite.SizeMultiplier, hero.Rotation.rotation);
            var coinRect = GetRotatedRectangle(coin.Position, coin.Sprite.Texture, coin.Sprite.SizeMultiplier, 0);
            return SATCollision.IsCollision(heroRect, coinRect);
        }

        public static bool IsCollision(Bullet bullet, Enemy enemy)
        {
            var bulletRect = GetRotatedRectangle(bullet.Position, bullet.Sprite.Texture, bullet.Sprite.SizeMultiplier, 0);
            var enemyRect = GetRotatedRectangle(enemy.Position, enemy.Sprite.Texture, enemy.Sprite.SizeMultiplier, enemy.Rotation.rotation);
            return SATCollision.IsCollision(bulletRect, enemyRect);
        }
    }
}

