using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class DropBehaviour : MonoBehaviour
    {
        [SerializeField] private HealthFeature healthFeature;
        [SerializeField] private LootFeature lootFeature;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No HealthFeature is defined");
            }
            
            healthFeature.onDeath.AddListener(OnDeath);
        }
        
        private void OnDeath()
        {
            foreach (var loot in lootFeature.GetLootTable())
            {
                var enemy = Instantiate(loot, gameObject.transform.position, Quaternion.identity);
            }

        }
    }
}