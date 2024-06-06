namespace AiFirst
{
    public static class GameSettingsData
    {
        public static float CoinSpawnInterval { get; } = 4f;
        public static int EnemyHealth { get; } = 150;
        public static int EnemyDamage { get; } = 10;
        public static float EnemyBaseSpeed { get; } = 140f;
        public static int EnemyMinSpawnDistance { get; } = 200;
        public static int HeroHealth { get; } = 30;
        public static float HeroBaseVelocity { get; } = 200f;
        public static float BulletBaseVelocity { get; } = 700f;
        public static int BulletBaseDamage { get; } = 50;
        public static float ShootInterval { get; } = 0.2f;
        public static float HeroSpeedMultiplier { get; set; } = 1f;
        public static float EnemySpeedMultiplier { get; set; } = 1f;
        public static float HeroSpeedIncreaser { get; set; } = 0.05f;
        public static float EnemySpeedIncreaser { get; set; } = 0.05f;
        public static float ShootSpeedMultiplier { get; set; } = 1f;
        public static float EnemySlowMultiplier { get; set; } = 1f;
        public static float ShootSpeedBoostTimer { get; set; } = 0f;
        public static float EnemySlowTimer { get; set; } = 0f;
        public static float EnemySpawnInterval { get; set; } = 1f;
        public static float EnemySpawnIntervalMultiplier { get; set; } = 1f;
    }
}
