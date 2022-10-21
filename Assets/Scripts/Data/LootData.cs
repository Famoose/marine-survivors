using System;
using UnityEngine;

namespace Data
{
    
    [Serializable]
    public class Loot
    {
        public float probability;
        public GameObject prefab;
    }
    
    [CreateAssetMenu(fileName = "LootData", menuName = "FeatureData/LootData", order = 0)]
    public class LootData : ScriptableObject
    {
        public Loot[] lootTable;
    }
}