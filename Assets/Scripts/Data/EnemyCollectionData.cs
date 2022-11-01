using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class EnemyData
    {
        public GameObject enemy;
        public EnemyConfig enemyConfig;

        public EnemyData Copy()
        {
            return new EnemyData
            {
                enemy = this.enemy,
                enemyConfig = this.enemyConfig
            };
        }
    }
    [CreateAssetMenu(fileName = "EnemyCollectionData", menuName = "FeatureData/EnemyCollectionData", order = 0)]
    public class EnemyCollectionData : ScriptableObject
    {
        public List<EnemyData> enemies;
    }
}