using System;
using System.Linq;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Feature
{
    public class LootFeature : MonoBehaviour
    {
        [SerializeField] private LootData initialData;
        private LootData _data;
        public bool IsInitialized { get; private set; }

        public void Initialize(LootData lootData)
        {
            if (lootData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<LootData>();
            _data.lootTable = lootData.lootTable.Select(l => l.Copy()).ToArray();

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public Loot[] GetLootTable()
        {
            if (IsInitialized)
            {
                return _data.lootTable.Where(l => Random.value < l.probability).ToArray();
            }
            return null;
        }
    }
}