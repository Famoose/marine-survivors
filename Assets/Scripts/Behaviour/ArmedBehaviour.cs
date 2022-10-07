using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Feature;
using Unity.VisualScripting;
using UnityEngine;

namespace Behaviour
{
    public class ArmedBehaviour : MonoBehaviour
    {
        [SerializeField] private List<OwnedWeaponFeature> activeWeapons;

        public void Awake()
        {
            if (activeWeapons == null || !activeWeapons.Any())
            {
                throw new ArgumentException("At least one weapon has to be active.");
            }
            
            foreach (OwnedWeaponFeature weaponFeature in activeWeapons)
            {
                InstantiateAndInitializeWeapon(weaponFeature);
            }
        }

        private void InstantiateAndInitializeWeapon(OwnedWeaponFeature weaponFeature)
        {
            GameObject weapon = Instantiate(weaponFeature.GetCurrentLevelPrefab(), GetComponent<Transform>());
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
            feature.Initialize(weaponFeature.GetCurrentLevelData());
        }
    }
}
