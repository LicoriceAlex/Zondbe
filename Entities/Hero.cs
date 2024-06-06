using System.Collections.Generic;

namespace AiFirst
{
    public class Hero
    {
        public Sprite Sprite;
        public Health Health;
        public Position Position;
        public Velocity Velocity;
        public PlayerInput? Input;
        public List<Bullet> Bullets = new List<Bullet>();
        public DamageCooldown DamageCooldown;
        public Rotation Rotation;
    }
}
