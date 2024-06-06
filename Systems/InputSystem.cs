using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AiFirst
{
    public class InputSystem
    {
        private ContentManager _content;
        private float _timeSinceLastShot = 0f;

        public InputSystem(ContentManager content)
        {
            _content = content;
        }

        public void Update(Hero hero, float deltaTime)
        {
            var keyboardState = Keyboard.GetState();

            if (hero.Input.HasValue)
            {
                var input = hero.Input.Value;

                hero.Velocity.X = 0;
                hero.Velocity.Y = 0;

                if (keyboardState.IsKeyDown(input.Up)) hero.Velocity.Y = -GameSettingsData.HeroBaseVelocity;
                if (keyboardState.IsKeyDown(input.Down)) hero.Velocity.Y = GameSettingsData.HeroBaseVelocity;
                if (keyboardState.IsKeyDown(input.Left)) hero.Velocity.X = -GameSettingsData.HeroBaseVelocity;
                if (keyboardState.IsKeyDown(input.Right)) hero.Velocity.X = GameSettingsData.HeroBaseVelocity;

                if (hero.Velocity.X != 0 || hero.Velocity.Y != 0)
                {
                    var velocity = new Vector2(hero.Velocity.X, hero.Velocity.Y);
                    velocity.Normalize();
                    hero.Velocity.X = velocity.X * GameSettingsData.HeroBaseVelocity;
                    hero.Velocity.Y = velocity.Y * GameSettingsData.HeroBaseVelocity;
                }
            }

            _timeSinceLastShot += deltaTime;
        }

        public void Shoot(Hero hero, Vector2 mousePosition)
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && _timeSinceLastShot >= GameSettingsData.ShootInterval / GameSettingsData.ShootSpeedMultiplier)
            {
                if (hero.Input.HasValue)
                {
                    var heroCenter = new Vector2(hero.Position.X + hero.Sprite.Texture.Width / 2, hero.Position.Y + hero.Sprite.Texture.Height / 2);
                    var direction = Vector2.Normalize(mousePosition - heroCenter);
                    var bulletStartPosition = heroCenter + direction * (hero.Sprite.Texture.Width / 2 + 20);
                    var bullet = new Bullet
                    {
                        Sprite = new Sprite { Texture = _content.Load<Texture2D>("bullet_normal"), SizeMultiplier = TextureSizesData.BulletSizeMultiplier },
                        Position = new Position { X = bulletStartPosition.X, Y = bulletStartPosition.Y },
                        Velocity = new Velocity { X = direction.X * GameSettingsData.BulletBaseVelocity * GameSettingsData.HeroSpeedMultiplier,
                            Y = direction.Y * GameSettingsData.BulletBaseVelocity * GameSettingsData.HeroSpeedMultiplier },
                        Damage = new Damage { damage = GameSettingsData.BulletBaseDamage }
                    };
                    hero.Bullets.Add(bullet);
                    _timeSinceLastShot = 0f;
                }
            }
        }
    }
}
