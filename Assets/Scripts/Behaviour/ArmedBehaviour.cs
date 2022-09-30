using System;
using System.Collections.Generic;
using System.Linq;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class ArmedBehaviour : MonoBehaviour
    {
        [SerializeField] private List<WeaponFeature> activeWeapons;

        public void Awake()
        {
            if (activeWeapons == null || !activeWeapons.Any())
            {
                throw new ArgumentException("At least one weapon has to be active.");
            }

            foreach (WeaponFeature weaponFeature in activeWeapons)
            {
                Instantiate(weaponFeature.GetCurrentLevelPrefab(), GetComponent<Transform>());
            }
        }
    }
}
