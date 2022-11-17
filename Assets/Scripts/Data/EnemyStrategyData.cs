using System;
using System.Linq;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BossConfig
    {
        public float lastBossSpawn = 0;
        public float bossSpawnOffset = 300;
        public float bossHealthMultiplier = 10;
        public float bossExpMultiplier = 10;
        public float bossDamageMultiplier = 2;
        public BossConfig Copy()
        {
            return new BossConfig
            {
                lastBossSpawn = this.lastBossSpawn,
                bossSpawnOffset = this.bossSpawnOffset,
                bossHealthMultiplier = this.bossHealthMultiplier,
                bossExpMultiplier = this.bossExpMultiplier,
                bossDamageMultiplier = this.bossDamageMultiplier
            };
        }
    }
    
    [Serializable]
    public class FunctionParameter
    {
        public float constant = 0;
        public float multiplier = 1;
        public FunctionParameter Copy()
        {
            return new FunctionParameter
            {
                constant = this.constant,
                multiplier = this.multiplier
            };
        }
    }

    [Serializable]
    public class PearlThreshold
    {
        public GameObject pearl;
        public float threshold;

        public PearlThreshold Copy()
        {
            return new PearlThreshold
            {
                pearl = this.pearl,
                threshold = this.threshold
            };
        }
    }
    
    [Serializable]
    public class Collectables
    {
        public PearlThreshold[] pearls;
        public GameObject[] items;
        public GameObject chest;

        public Collectables Copy()
        {
            return new Collectables
            {
                pearls = this.pearls.Select(p => p.Copy()).ToArray(),
                chest = this.chest,
                items = this.items
            };
        }
    }
    
    [CreateAssetMenu(fileName = "EnemyStrategyData", menuName = "FeatureData/EnemyStrategyData", order = 0)]
    public class EnemyStrategyData : ScriptableObject
    {
        public float baseDifficulty = 0.5f;
        public BossConfig bossConfig;
        public Collectables collectables;
        public FunctionParameter healthFunction;
        public FunctionParameter expFunction;
        public FunctionParameter damageFunction;
        public FunctionParameter amountFunction;
        public float walkSpeed = 5;
        public float flySpeed = 3;
        public float strategySleep = 5;
        public float lastStrategyEvaluation = 0;
        public float startTime = 0;
        public float flyProbability = 0.2f;
        public float cutDirectionProbability = 0.5f;
        public float itemDropBaseProbability = 0.05f;
        public float spawnThreshold = 5;
    }
}