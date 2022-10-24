using System;
using Data;
using Feature;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class EnemySpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyWaveFeature enemyWaveFeature;
        [SerializeField] private TrackingFeature trackingFeature;

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
        }

        private void Update()
        {
            EnemyConfig ec = enemyWaveFeature.GetNextWave(Time.time);
            GameObject player = trackingFeature.GetTarget();
            if (ec != null && player)
            {
                Vector2 playerPosition = player.transform.position;
                for (int i = 0; i < ec.amount; i++)
                {
                    Vector2 direction = Random.insideUnitCircle.normalized;
                    Vector2 spawnPosition = playerPosition + direction * ec.positionOffset;
                    enemyWaveFeature.InstantiateEnemy(ec, spawnPosition, player);
                }
            }
        }
    }
}