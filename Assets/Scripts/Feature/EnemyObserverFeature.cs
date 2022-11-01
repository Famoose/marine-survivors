using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class EnemyObserverFeature : MonoBehaviour
    {
        [SerializeField] private EnemyCollectionData initialData;
        private EnemyCollectionData _data;
        public UnityEvent<EnemyData> onEnemyDeleted;

        public bool IsInitialized { get; private set; }

        public void Initialize(EnemyCollectionData collectionData)
        {
            if (collectionData == null)
            {
                throw new ArgumentException("initialData was null");
            }

            _data = ScriptableObject.CreateInstance<EnemyCollectionData>();
            _data.enemies = collectionData.enemies.Select(e => e.Copy()).ToList();

            IsInitialized = true;
        }

        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public void AddEnemy(GameObject enemy, EnemyConfig ec)
        {
            HealthFeature healthFeature = enemy.GetComponent<HealthFeature>();
            if (!healthFeature)
            {
                throw new ArgumentException("enemy had no health feature");
            }

            healthFeature.onDeath.AddListener(() => RemoveEnemy(enemy));
            _data.enemies.Add(new EnemyData{enemy = enemy, enemyConfig = ec});
        }

        public List<EnemyData> GetAllActiveEnemies()
        {
            return _data.enemies;
        }

        private EnemyData RemoveEnemy(GameObject enemy)
        {
            var index = _data.enemies.FindIndex(ed => ed.enemy == enemy);
            var deletedEnemy = _data.enemies[index];
            _data.enemies.RemoveAt(index);
            return deletedEnemy;
        }
        
        public void DeleteEnemy(GameObject enemy)
        {
            var removeEnemy = RemoveEnemy(enemy);
            onEnemyDeleted.Invoke(removeEnemy);
            Destroy(enemy);
        }
    }
}