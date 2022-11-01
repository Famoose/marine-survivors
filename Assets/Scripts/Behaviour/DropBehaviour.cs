using System;
using Data;
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
                var lootGameObject = Instantiate(loot.prefab, gameObject.transform.position, Quaternion.identity);
                //if override data is set, get the collectable type and try to map the override accordingly.
                //currently only implemented for pearls to let the exp be set.
                if (loot.overrideData)
                {
                    CollectableFeature collectableFeature = lootGameObject.GetComponent<CollectableFeature>();
                    if (collectableFeature)
                    {
                        CollectableType collectableType = collectableFeature.GetCollectableType();
                        if (collectableType == CollectableType.Pearl)
                        {
                            LevelFeature levelFeature = lootGameObject.GetComponent<LevelFeature>();
                            levelFeature.Initialize((LevelData) loot.overrideData);
                        }
                    }
                }
            }
        }
    }
}