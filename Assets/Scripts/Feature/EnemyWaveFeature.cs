using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Feature
{
    public class EnemyWaveFeature : MonoBehaviour
    {
        [SerializeField] private EnemyWaveConfigData initialData;
        private EnemyWaveConfigData _data;
        private float _nextWave = 0.0f;
        private int _currentWave = 0;

        public bool IsInitialized { get; private set; }

        public void Initialize(EnemyWaveConfigData waveConfigData)
        {
            if (waveConfigData == null)
            {
                throw new ArgumentException("initialData was null");
            }

            _data = ScriptableObject.CreateInstance<EnemyWaveConfigData>();
            _data.enemyConfigs = waveConfigData.enemyConfigs.Select(ec => ec.Copy()).ToList();

            IsInitialized = true;
        }

        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public EnemyConfig GetNextWave(float timeOffset)
        {
            if (timeOffset >= _nextWave)
            {
                if (_currentWave < _data.enemyConfigs.Count)
                {
                    var configEnemyConfig = _data.enemyConfigs[_currentWave];
                    _nextWave = timeOffset + _data.enemyConfigs[_currentWave].timeOffset;
                    _currentWave++;
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
            var playerTrackingFeature = enemy.GetComponent<TrackingFeature>();
            var lootFeature = enemy.GetComponent<LootFeature>();
            var healthFeature = enemy.GetComponent<HealthFeature>();

            enemy.transform.localScale *= enemyConfig.sizeMultiplier;

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
                playerTrackingFeature.SetTarget(player);
            }

            if (lootFeature && enemyConfig.lootOverride)
            {
                lootFeature.Initialize(enemyConfig.lootOverride);
            }

            if (healthFeature && enemyConfig.healthData)
            {
                healthFeature.Initialize(enemyConfig.healthData);
            }

            return enemy;
        }

        public void AddWaves(List<EnemyConfig> newEnemies)
        {
            newEnemies.ForEach(ne => _data.enemyConfigs.Add(ne));
        }
    }
}