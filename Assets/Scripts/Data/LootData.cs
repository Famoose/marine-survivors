using System;
using UnityEngine;

namespace Data
{
    
    [Serializable]
    public class Loot
    {
        public float probability;
        public GameObject prefab;
        public ScriptableObject overrideData;

        public Loot Copy()
        {
            return new Loot
            {
                probability = this.probability,
                prefab = this.prefab,
                overrideData = this.overrideData
            };
        }
    }
    
    [CreateAssetMenu(fileName = "LootData", menuName = "FeatureData/LootData", order = 0)]
    public class LootData : ScriptableObject
    {
        public Loot[] lootTable;
    }
}