using System;
using Data;
using Feature;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class EnemySpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyWaveFeature enemyWaveFeature;
        [SerializeField] private PlayerTrackingFeature playerTrackingFeature;

        private void Awake()
        {
            if (enemyWaveFeature == null)
            {
                throw new ArgumentException("No enemyWaveFeature is defined");
            }

            if (playerTrackingFeature == null)
            {
                throw new ArgumentException("No playerTrackingFeature is defined");
            }
        }

        private void Update()
        {
            EnemyConfig ec = enemyWaveFeature.GetNextWave(Time.time);
            GameObject player = playerTrackingFeature.GetPlayer();
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