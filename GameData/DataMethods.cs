using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiFirst
{
    public static class DataMethods
    {
        public static void ResetData()
        {
            Data.Score = 0;
            Data.CoinsCollected = 0;
            GameSettingsData.HeroSpeedMultiplier = 1f;
            GameSettingsData.EnemySpeedMultiplier = 1f;
            GameSettingsData.HeroSpeedIncreaser = 0.05f;
            GameSettingsData.EnemySpeedIncreaser = 0.05f;
            GameSettingsData.ShootSpeedMultiplier = 1f;
            GameSettingsData.EnemySlowMultiplier = 1f;
            GameSettingsData.ShootSpeedBoostTimer = 0f;
            GameSettingsData.EnemySlowTimer = 0f;
            GameSettingsData.EnemySpawnInterval = 1f;
        }
    }
}
