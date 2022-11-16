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
        public HealthData healthData;
        public Vector2 direction = Vector2.zero;
        public Vector2 sizeMultiplier = Vector2.one;
        
        public EnemyConfig Copy()
        {
            return new EnemyConfig
            {
                movementOverride = this.movementOverride,
                inflictDamageOverride = this.inflictDamageOverride,
                lootOverride = this.lootOverride,
                amount = this.amount,
                enemy = this.enemy,
                timeOffset = this.timeOffset,
                positionOffset = this.positionOffset,
                healthData = this.healthData,
                direction = this.direction,
                sizeMultiplier = this.sizeMultiplier
            };
        }
    }
    [CreateAssetMenu(fileName = "EnemyWaveConfigData", menuName = "FeatureData/EnemyWaveConfigData", order = 1)]
    public class EnemyWaveConfigData : ScriptableObject
    {
        public List<EnemyConfig> enemyConfigs;
    }
}