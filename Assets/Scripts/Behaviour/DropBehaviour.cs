using System;
using Data;
using Feature;
using UnityEngine;
using Random = UnityEngine.Random;

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
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 spawnPosition = new Vector3(
                    currentPosition.x + Random.Range(0f, 1f), 
                    currentPosition.y + Random.Range(0f, 1f), 
                    currentPosition.z);
                
                var lootGameObject = Instantiate(loot.prefab, spawnPosition, Quaternion.identity);
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