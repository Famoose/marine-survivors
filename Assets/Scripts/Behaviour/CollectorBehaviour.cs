using System;
using System.Collections.Generic;
using Behaviour.item;
using Data;
using Feature;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class CollectorBehaviour : MonoBehaviour
    {
        [SerializeField] private RadiusFeature radiusFeature;
        [SerializeField] private HealthFeature healthFeature;
        [SerializeField] private LevelFeature levelFeature;
        [SerializeField] private OwnedWeaponFeature ownedWeapon;
        [SerializeField] private AbilityFeature abilityFeature;
        private CircleCollider2D _collider;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No HealthFeature is defined");
            }

            if (radiusFeature == null)
            {
                throw new ArgumentException("No RadiusFeature is defined");
            }

            if (levelFeature == null)
            {
                throw new ArgumentException("No LevelFeature is defined");
            }
            
            if (ownedWeapon == null)
            {
                throw new ArgumentException("No OwnedWeapon is defined");
            }

            if (abilityFeature == null)
            {
                throw new ArgumentException("No AbilityFeature is defined");
            }
        }

        private void Start()
        {
            _collider = gameObject.AddComponent<CircleCollider2D>();
            _collider.radius = radiusFeature.GetRadius();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Collectable"))
            {
                var collectableFeature = other.gameObject.GetComponent<CollectableFeature>();
                if (collectableFeature == null)
                {
                    return;
                }
                
                CollectableType type = collectableFeature.GetCollectableType();
                
                if (type.Equals(CollectableType.Pearl))
                {
                    var pearlLevelFeature = other.gameObject.GetComponent<LevelFeature>();
                    if (pearlLevelFeature != null)
                    {
                        levelFeature.AddExperience(pearlLevelFeature.GetExperiencePointsTotal());
                    }
                }

                if (type.Equals(CollectableType.Chest))
                {
                    CollectChest();
                }

                if (type.Equals(CollectableType.Item))
                {
                    IItemBehaviour itemBehaviour = other.gameObject.GetComponent<IItemBehaviour>();
                    if (itemBehaviour != null)
                    {
                        itemBehaviour.ActivateItem(gameObject);
                    }
                    else
                    {
                        throw new ArgumentException("No itemBehaviour on collectable is defined");
                    }
                }
                
                Destroy(other.gameObject);
            }
        }

        private void CollectChest()
        {
            try
            {
                List<AbilityData> activeAbilities = abilityFeature.GetActiveAbilities();
                //randomize upgrade between abilities and weapon
                if (Random.value < 0.5f && activeAbilities.Count > 0)
                {
                    abilityFeature.LevelUpAbility(activeAbilities[Random.Range(0, activeAbilities.Count)]);
                }
                else
                {
                    List<WeaponLevelData> activeWeapons = ownedWeapon.GetActiveWeapons();
                    ownedWeapon.LevelUpWeapon(activeWeapons[Random.Range(0, activeAbilities.Count)].weaponName);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}