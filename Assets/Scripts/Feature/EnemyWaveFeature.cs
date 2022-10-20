using System;
using Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Feature
{
    public class EnemyWaveFeature : MonoBehaviour
    {
        [SerializeField] private EnemyWaveConfigData config;
        private float _nextWave = 0.0f;
        private int _currentWave = 0;

        private void Awake()
        {
            if (config == null)
            {
                throw new ArgumentException("config was null");
            }
        }

        public EnemyConfig GetNextWave(float timeOffset)
        {
            if (timeOffset >= _nextWave)
            {
                if (_currentWave < config.enemyConfigs.Count)
                {
                    //stay on the last if out of range
                    var configEnemyConfig = config.enemyConfigs[_currentWave];
                    if (_currentWave + 1 < config.enemyConfigs.Count)
                    {
                        _currentWave++;
                    }

                    _nextWave = timeOffset + config.enemyConfigs[_currentWave].timeOffset;
                    return configEnemyConfig;
                }
            }

            return null;
        }

        public GameObject InstantiateEnemy(EnemyConfig enemyConfig, Vector2 position, GameObject player)
        {
            var enemy = Instantiate(enemyConfig.enemy, position, Quaternion.identity);
            var movementFeature = enemy.GetComponent<MovementFeature>();
            var inflictDamageOnCollisionFeature = enemy.GetComponent<InflictDamageOnCollisionFeature>();
            var playerTrackingFeature = enemy.GetComponent<PlayerTrackingFeature>();
            
            if (movementFeature && enemyConfig.movementOverride)
            {
                movementFeature.Initialize(enemyConfig.movementOverride);
            }
            
            if (inflictDamageOnCollisionFeature && enemyConfig.inflictDamageOverride)
            {
                inflictDamageOnCollisionFeature.Initialize(enemyConfig.inflictDamageOverride);
            }

            if (playerTrackingFeature && player)
            {
                playerTrackingFeature.SetPlayer(player);
            }

            return gameObject;
        }
    }
}