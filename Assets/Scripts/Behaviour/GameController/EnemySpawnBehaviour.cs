using System;
using Data;
using Data.Enum;
using Feature;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class EnemySpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyWaveFeature enemyWaveFeature;
        [SerializeField] private TrackingFeature trackingFeature;
        [SerializeField] private EnemyObserverFeature enemyObserverFeature;
        private GameObject _target;

        private void Awake()
        {
            if (enemyWaveFeature == null)
            {
                throw new ArgumentException("No enemyWaveFeature is defined");
            }

            if (trackingFeature == null)
            {
                throw new ArgumentException("No playerTrackingFeature is defined");
            }
            
            if (enemyObserverFeature == null)
            {
                throw new ArgumentException("No enemyObserverFeature is defined");
            }
        }

        private void Start()
        { 
            _target = trackingFeature.GetTarget();
            enemyObserverFeature.onEnemyDeleted.AddListener(RespawnDeletedEnemy);
        }

        private void RespawnDeletedEnemy(EnemyData enemyData)
        {
            Vector2 playerPosition = _target.transform.position;
            Debug.Log("Respawn");
            if (enemyData.enemyConfig.movementOverride.movementType == MovementType.Constant)
            {
                enemyData.enemyConfig.movementOverride.movement = Vector2.zero;
            }
            SpawnEnemy(enemyData.enemyConfig, playerPosition);
        }

        private void Update()
        {
            EnemyConfig ec = enemyWaveFeature.GetNextWave(Time.time);
            if (ec != null && _target)
            {
                Vector2 playerPosition = _target.transform.position;
                for (float i = 0; i < ec.amount; i += 1)
                {
                    SpawnEnemy(ec, playerPosition);
                }
            }
        }

        private void SpawnEnemy(EnemyConfig ec, Vector2 playerPosition)
        {
            Vector2 direction = ec.direction;
            if (direction == Vector2.zero)
            {
                direction = Random.insideUnitCircle.normalized;
            }

            Vector2 spawnPosition = playerPosition + direction * ec.positionOffset;
            GameObject enemy = enemyWaveFeature.InstantiateEnemy(ec, spawnPosition, _target);
            enemyObserverFeature.AddEnemy(enemy, ec);
        }
    }
}