using System;
using Data;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class ArmedBehaviour : MonoBehaviour
    {
        [SerializeField] private OwnedWeaponFeature ownedWeaponFeature;
        [SerializeField] private AbilityFeature abilityFeature;

        public void Awake()
        {
            if (ownedWeaponFeature == null)
            {
                throw new ArgumentException("No ownedWeaponFeature is defined.");
            }
            
            if (abilityFeature == null)
            {
                throw new ArgumentException("No abilityFeature is defined.");
            }
            
            ownedWeaponFeature.onWeaponActivated.AddListener(InstantiateAndInitializeWeapon);
        }

        private void InstantiateAndInitializeWeapon(WeaponLevelData weaponLevelData)
        {
            WeaponData currentLevelWeaponData = weaponLevelData.GetCurrentLevelsWeaponData();
            GameObject weapon = Instantiate(currentLevelWeaponData.prefab, GetComponent<Transform>());
            WeaponBehaviour weaponBehaviour = weapon.GetComponent<WeaponBehaviour>();
            if (weaponBehaviour == null)
            {
                Destroy(weapon);
                throw new ArgumentException("The instantiated weapon does not contain a WeaponBehaviour.");
            }
            WeaponFeature feature = weaponBehaviour.GetComponent<WeaponFeature>();
            if (feature == null)
            {
                Destroy(weapon);
                throw new ArgumentException("The instantiated weapon does not contain a WeaponFeature on the Behaviour.");
            }
            feature.Initialize(currentLevelWeaponData);
        }
    }
}
