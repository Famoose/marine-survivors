using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class EnemyCountBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyCountFeature enemyCountFeature;
        [SerializeField] private EnemyObserverFeature enemyObserverFeature;

        private void Awake()
        {
            if (enemyCountFeature == null)
            {
                throw new ArgumentException("No enemyCountFeature is defined");
            }

            if (enemyObserverFeature == null)
            {
                throw new ArgumentException("No enemyObserverFeature is defined");
            }
        }

        private void Start()
        {
            enemyObserverFeature.onEnemyDeath.AddListener(IncrementEnemyCount);
        }

        private void IncrementEnemyCount(GameObject enemy)
        {
            enemyCountFeature.AddEnemyKilled(1);
        }
    }
}