using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AiFirst
{
    public class CoinSpawnSystem
    {
        private Random _random = new Random();
        private ContentManager _content;
        private float _timeSinceLastSpawn = 0f;

        public CoinSpawnSystem(ContentManager content)
        {
            _content = content;
        }

        public void Update(List<Coin> coins, float deltaTime)
        {
            _timeSinceLastSpawn += deltaTime;
            if (_timeSinceLastSpawn >= GameSettingsData.CoinSpawnInterval)
            {
                _timeSinceLastSpawn = 0f;
                CoinType.Type coinType;
                double randomValue = _random.NextDouble();
                if (randomValue < 0.8)
                    coinType = CoinType.Type.Normal;             
                else if (randomValue < 0.9)              
                    coinType = CoinType.Type.ShootSpeedBoost;              
                else             
                    coinType = CoinType.Type.EnemySlow;               
                var texture = coinType == CoinType.Type.Normal ? _content.Load<Texture2D>("coin_normal") :
                              coinType == CoinType.Type.ShootSpeedBoost ? _content.Load<Texture2D>("coin_shoot_speed") :
                              _content.Load<Texture2D>("coin_slow");
                var coin = new Coin
                {
                    CointType = new CoinType { coinType = coinType },
                    Sprite = new Sprite { Texture = texture, SizeMultiplier = TextureSizesData.CoinSizeMultiplier},
                    Position = new Position
                    {
                        X = _random.Next(0, Data.ScreenWidth - texture.Width * (int)TextureSizesData.CoinSizeMultiplier),
                        Y = _random.Next(0, Data.ScreenHeight - texture.Width * (int)TextureSizesData.CoinSizeMultiplier)
                    },
                    SpawnTime = new SpawnTime { spawnTime = 0f }
                };
                coins.Add(coin);
            }
            foreach (var coin in coins)
            {
                coin.SpawnTime.spawnTime += deltaTime;
            }
        }
    }
}
