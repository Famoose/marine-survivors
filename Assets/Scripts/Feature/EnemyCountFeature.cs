using System;
using Data;
using UnityEngine;

namespace Feature
{
    public class EnemyCountFeature : MonoBehaviour
    {
        [SerializeField] private EnemyCountData initialData;
        private EnemyCountData _data;
        public bool IsInitialized { get; private set; }

        public void Initialize(EnemyCountData enemyCountData)
        {
            if (enemyCountData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<EnemyCountData>();
            _data.enemyKilled = enemyCountData.enemyKilled;

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public int GetEnemyKilled()
        {
            if (IsInitialized)
            {
                return _data.enemyKilled;
            }
            throw new ArgumentException("feature data was not initialized");
        }
        
        public void AddEnemyKilled(int count)
        {
            if (IsInitialized)
            {
                _data.enemyKilled += count;
                return;
            }
            throw new ArgumentException("feature data was not initialized");
        }
    }
}