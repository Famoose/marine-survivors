using System;
using Data;
using Data.Enum;
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
            for (int i = 0; i < weaponFeature.GetWeaponData().projectileAmount; i++)
            {
                ShootProjectile();
            }
        }

        private void ShootProjectile()
        { 
            GameObject projectile = Instantiate(weaponFeature.GetProjectilePrefab(), _currentTransform);
            projectile.transform.parent = _currentTransform.parent.parent;
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
            InflictDamageOnCollisionBehaviour inflictDamageOnCollisionBehaviour =
                projectile.GetComponent<InflictDamageOnCollisionBehaviour>();
            if (inflictDamageOnCollisionBehaviour == null)
            {
                Destroy(projectile);
                throw new ArgumentException("The instantiated projectile does not contain a InflictDamageOnCollisionBehaviour.");
            }
            
            // Features are null-checked at their corresponding behaviours
            ReducibleFeature reducibleFeature = disappearingBehaviour.GetReducibleFeature();
            MovementFeature movementFeature = movementBehaviour.GetMovementFeature();
            InflictDamageOnCollisionFeature inflictDamageOnCollisionFeature =
                inflictDamageOnCollisionBehaviour.GetInflictDamageOnCollisionFeature();

            ReducibleData reducibleData = ScriptableObject.CreateInstance<ReducibleData>();
            reducibleData.value = weaponFeature.GetWeaponData().projectileLifetime;
            reducibleFeature.Initialize(reducibleData);

            MovementData movementData = ScriptableObject.CreateInstance<MovementData>();
            movementData.movement = weaponFeature.GetWeaponData().projectileInitialMovementDirection;
            movementData.speed = weaponFeature.GetWeaponData().projectileSpeed;
            movementData.movementType = weaponFeature.GetWeaponData().projectileMovementType;
            movementData.initialMovementType = weaponFeature.GetWeaponData().projectileInitialMovementType;
            movementFeature.Initialize(movementData, transform.parent.localScale);

            InflictDamageData inflictDamageData = ScriptableObject.CreateInstance<InflictDamageData>();
            inflictDamageData.inflictedDamage = weaponFeature.GetWeaponData().projectileInflictedDamage;
            inflictDamageData.destroyOnInflictingDamage = true;
            inflictDamageData.ignoredGameObjectType = ActiveGameObjectType.Player;
            inflictDamageOnCollisionFeature.Initialize(inflictDamageData);
        }
    }
}