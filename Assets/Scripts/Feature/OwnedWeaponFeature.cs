using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class OwnedWeaponFeature : MonoBehaviour
    {
        [SerializeField] private List<WeaponLevelData> activeWeapons;
        [SerializeField] private List<WeaponLevelData> availableWeapons;
        [SerializeField] private int maximumWeapons = 4;
        public UnityEvent<WeaponLevelData> onWeaponLevelUp;
        public UnityEvent<WeaponLevelData> onWeaponActivated;

        private void Awake()
        {
            activeWeapons = activeWeapons.Select(InitializeWeapon).ToList();
            availableWeapons = availableWeapons.Select(InitializeWeapon).ToList();
        }

        private WeaponLevelData InitializeWeapon(WeaponLevelData initialData)
        {
            if (initialData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            WeaponLevelData weaponLevelData = ScriptableObject.CreateInstance<WeaponLevelData>();
            weaponLevelData.weaponName = initialData.weaponName;
            weaponLevelData.perLevelWeaponData = initialData.perLevelWeaponData.Select(vm => vm.Copy()).ToList();

            return weaponLevelData;
        }
        
        private void Start()
        {
            foreach (WeaponLevelData weaponLevelData in activeWeapons)
            {
                onWeaponActivated.Invoke(weaponLevelData);
            }
        }

        public List<WeaponLevelData> GetActiveWeapons()
        {
            return activeWeapons;
        }

        public List<WeaponLevelData> GetAvailableWeapons()
        {
            return availableWeapons;
        }

        public void LevelUpWeapon(string weaponName)
        {
            WeaponLevelData weaponLevelData = activeWeapons.SingleOrDefault(data => data.weaponName == weaponName);
            if (weaponLevelData == null)
            {
                throw new ArgumentException("Weapon is not active");
            }

            weaponLevelData.IncreaseLevel();
            onWeaponLevelUp.Invoke(weaponLevelData);
        }

        public void ActivateWeapon(string weaponName)
        {
            if (activeWeapons.Count == maximumWeapons)
            {
                throw new ArgumentException("Maximum weapons active reached.");
            }

            WeaponLevelData weaponLevelData = availableWeapons.SingleOrDefault(data => data.weaponName == weaponName);
            if (weaponName == null)
            {
                throw new ArgumentException("Weapon is not available");
            }
            activeWeapons.Add(weaponLevelData);
            availableWeapons.Remove(weaponLevelData);
            onWeaponActivated.Invoke(weaponLevelData);
        }
    }
}