using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Data;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private WeaponFeature weaponFeature;

        private Transform _currentTransform;
        
        private void Awake()
        {
            if (weaponFeature == null)
            {
                throw new ArgumentException("No WeaponFeature is defined");
            }

            _currentTransform = GetComponent<Transform>();
            
            weaponFeature.CooledDown += WeaponFeatureOnCooledDown;
        }

        private void FixedUpdate()
        {
            if (weaponFeature.IsInitialized)
            {
                ReduceWeaponCoolDownValue(Time.fixedDeltaTime);   
            }
        }
        
        private void ReduceWeaponCoolDownValue(float reduceAmount)
        {
            weaponFeature.ReduceCurrentCoolDownValue(reduceAmount);
        }

        private void WeaponFeatureOnCooledDown(object sender, EventArgs e)
        {
            GameObject projectile = Instantiate(weaponFeature.GetProjectilePrefab(), _currentTransform);
            DisappearingBehaviour disappearingBehaviour = projectile.GetComponent<DisappearingBehaviour>();
            if (disappearingBehaviour == null)
            {
                Destroy(projectile);
                throw new ArgumentException("The instantiated projectile does not contain a DisappearingBehaviour.");
            }
            MovementBehaviour movementBehaviour = projectile.GetComponent<MovementBehaviour>();
            if (movementBehaviour == null)
            {
                Destroy(projectile);
                throw new ArgumentException("The instantiated projectile does not contain a MovementBehaviour.");
            }
            ReducibleFeature reducibleFeature = disappearingBehaviour.GetComponent<ReducibleFeature>();
            if (reducibleFeature == null)
            {
                Destroy(projectile);
                throw new ArgumentException("The instantiated projectile does not contain a ReducibleFeature on the Behaviour.");
            }
            MovementFeature movementFeature = disappearingBehaviour.GetComponent<MovementFeature>();
            if (movementFeature == null)
            {
                Destroy(projectile);
                throw new ArgumentException("The instantiated projectile does not contain a MovementFeature on the Behaviour.");
            }
            
            ReducibleData reducibleData = ScriptableObject.CreateInstance<ReducibleData>();
            reducibleData.value = weaponFeature.GetWeaponData().projectileLifetime;
            reducibleFeature.Initialize(reducibleData);

            MovementData movementData = ScriptableObject.CreateInstance<MovementData>();
            movementData.movement = weaponFeature.GetWeaponData().projectileInitialMovementDirection;
            movementData.speed = weaponFeature.GetWeaponData().projectileSpeed;
            movementData.movementType = weaponFeature.GetWeaponData().projectileMovementType;
            movementData.initialMovementType = weaponFeature.GetWeaponData().projectileInitialMovementType;
            movementFeature.Initialize(movementData);
        }
    }
}