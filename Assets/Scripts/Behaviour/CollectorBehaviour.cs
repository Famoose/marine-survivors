using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private GameStateFeature gameStateFeature;
        private CircleCollider2D _collider;
        private GameObject _collectInfoMenu;

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
            
            if (gameStateFeature == null)
            {
                throw new ArgumentException("No GameStateFeature is defined");
            }
        }

        private void Start()
        {
            _collectInfoMenu = transform.Find("CollectInfoMenuCanvas").gameObject;
            _collectInfoMenu.SetActive(false);
            
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
                List<AbilityData> activeNotMaxLevelAbilities = abilityFeature.GetActiveAbilities()
                    .Where(a => !a.IsMaxLevel()).ToList();
                List<WeaponLevelData> activeNotMaxLevelWeapons = ownedWeapon.GetActiveWeapons()
                    .Where(w => !w.IsMaxLevel()).ToList();
                
                //randomize upgrade between abilities and weapon
                if ((!activeNotMaxLevelWeapons.Any() || Random.value < 0.5f) && activeNotMaxLevelAbilities.Any())
                {
                    AbilityData abilityToLevelUp = 
                        activeNotMaxLevelAbilities[Random.Range(0, activeNotMaxLevelAbilities.Count)];
                    int currentLevel = abilityToLevelUp.GetLevel();
                    abilityFeature.LevelUpAbility(abilityToLevelUp);
                    ShowCollectInfoMenu(abilityToLevelUp.abilityName, currentLevel);
                }
                else if (activeNotMaxLevelWeapons.Any())
                {
                    WeaponLevelData weaponToLevelUp =
                        activeNotMaxLevelWeapons[Random.Range(0, activeNotMaxLevelWeapons.Count)];
                    int currentLevel = weaponToLevelUp.GetLevel();
                    ownedWeapon.LevelUpWeapon(weaponToLevelUp.weaponName);
                    ShowCollectInfoMenu(weaponToLevelUp.weaponName, currentLevel);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }

        private void ShowCollectInfoMenu(string abilityOrWeaponName, int oldLevel)
        {
            _collectInfoMenu.GetComponent<CollectInfoMenuBehaviour>()
                .PrepareMenu(abilityOrWeaponName, oldLevel);
            
            // Pause game and show collect info menu
            gameStateFeature.PauseGame();
            _collectInfoMenu.SetActive(true);
        }
    }
}