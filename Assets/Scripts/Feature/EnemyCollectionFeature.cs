using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Feature
{
    public class EnemyCollectionFeature : MonoBehaviour
    {
        [SerializeField] private EnemyCollectionData initialData;
        private EnemyCollectionData _data;
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

        public List<EnemyData> GetAvailableEnemies()
        {
            return _data.enemies;
        }

        public EnemyData GetRandomEnemy()
        {
            return _data.enemies[Random.Range(0, _data.enemies.Count)];
        }
    }
}