using System;
using System.Collections.Generic;
using Data;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class ArmedBehaviour : MonoBehaviour
    {
        [SerializeField] private OwnedWeaponFeature ownedWeaponFeature;
        [SerializeField] private AbilityFeature abilityFeature;
        private Dictionary<WeaponLevelData, GameObject> _initializedWeapons = new Dictionary<WeaponLevelData, GameObject>();
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
            ownedWeaponFeature.onWeaponLevelUp.AddListener(UpdateWeapon);
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
            
            _initializedWeapons.Add(weaponLevelData, weapon);
        }

        private void UpdateWeapon(WeaponLevelData weaponLevelData)
        {
            WeaponFeature feature =  _initializedWeapons[weaponLevelData].GetComponent<WeaponFeature>();
            Debug.Log(weaponLevelData.GetCurrentLevelsWeaponData().coolDownTime);
            feature.Initialize(weaponLevelData.GetCurrentLevelsWeaponData());
        }
    }
}
