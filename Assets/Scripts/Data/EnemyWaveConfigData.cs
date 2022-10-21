using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class EnemyConfig
    {
        public MovementData movementOverride;
        public InflictDamageData inflictDamageOverride;
        public LootData lootOverride;
        public float amount = 10;
        public GameObject enemy;
        public float timeOffset;
        public float positionOffset = 20;
    }
    [CreateAssetMenu(fileName = "EnemyWaveConfigData", menuName = "FeatureData/EnemyWaveConfigData", order = 1)]
    public class EnemyWaveConfigData : ScriptableObject
    {
        public List<EnemyConfig> enemyConfigs;
    }
}