using System;
using System.Collections.Generic;
using System.Linq;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private WeaponFeature weaponFeature;
        [SerializeField] private GameObject projectile;

        private Transform _currentTransform;
        
        private void Awake()
        {
            if (weaponFeature == null)
            {
                throw new ArgumentException("No WeaponFeature is defined");
            }
            if (projectile == null)
            {
                throw new ArgumentException("No projectile is defined");
            }

            _currentTransform = GetComponent<Transform>();
            
            weaponFeature.CooledDown += WeaponFeatureOnCooledDown;
        }

        private void FixedUpdate()
        {
            ReduceWeaponCoolDownValue(Time.fixedDeltaTime);
        }
        
        private void ReduceWeaponCoolDownValue(float reduceAmount)
        {
            weaponFeature.ReduceCurrentCoolDownValue(reduceAmount);
        }

        private void WeaponFeatureOnCooledDown(object sender, EventArgs e)
        {
            if (weaponFeature.GetCurrentCoolDownValue() <= 0f)
            {
                Instantiate(projectile, _currentTransform);
            }
        }
    }
}