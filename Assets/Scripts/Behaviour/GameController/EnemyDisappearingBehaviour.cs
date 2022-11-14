using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class EnemyDisappearingBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyObserverFeature enemyObserverFeature;
        [SerializeField] private TrackingFeature trackingFeature;
        private float _enemyCheckOffset = 5;
        private float _lastEnemyCheck = 0;
        public float destroyRadius = 20;

        private void Awake()
        {
            if (enemyObserverFeature == null)
            {
                throw new ArgumentException("No enemyObserverFeature is defined");
            }

            if (trackingFeature == null)
            {
                throw new ArgumentException("No playerTrackingFeature is defined");
            }
        }

        private void Update()
        {
            var target = trackingFeature.GetTarget();
            if (target && Time.time > _lastEnemyCheck + _enemyCheckOffset)
            {
                Vector3 playerPosition = target.transform.position;
                
                //iterate from reverse to save remove while iterating
                var allActiveEnemies = enemyObserverFeature.GetAllActiveEnemies();
                for (int i = allActiveEnemies.Count - 1; i >= 0; i--)
                {
                    var activeEnemy = allActiveEnemies[i];
                    //could be already destroyed
                    if (activeEnemy != null)
                    {
                        Vector2 distance = playerPosition - activeEnemy.enemy.transform.position;
                        if (distance.magnitude > destroyRadius)
                        {
                            enemyObserverFeature.DeleteEnemy(activeEnemy.enemy);
                        }
                    }
                }
                _lastEnemyCheck = Time.time;
            }
        }
    }
}