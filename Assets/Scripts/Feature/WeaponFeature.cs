using System;
using System.Linq;
using Data;
using UnityEngine;

namespace Feature
{
    public class WeaponFeature : MonoBehaviour
    {
        private WeaponLevelData.WeaponData _weaponData;
        public bool IsInitialized { get; private set; }

        public void Initialize(WeaponLevelData.WeaponData weaponData)
        {
            if (weaponData == null)
            {
                throw new ArgumentException("The weapon data is not set.");
            }

            _weaponData = new WeaponLevelData.WeaponData
            {
                coolDownTime = weaponData.coolDownTime,
                projectileSpeed = weaponData.projectileSpeed,
                projectileMovementType = weaponData.projectileMovementType,
                projectileInitialMovementType = weaponData.projectileInitialMovementType,
                projectileInitialMovementDirection = weaponData.projectileInitialMovementDirection,
                projectileLifetime = weaponData.projectileLifetime,
                prefab = weaponData.prefab,
                projectilePrefab = weaponData.projectilePrefab
            };
            ResetCurrentCoolDownValue();
            IsInitialized = true;
        }
        
        public event EventHandler<EventArgs> CooledDown;

        public WeaponLevelData.WeaponData GetWeaponData()
        {
            return _weaponData;
        }

        public GameObject GetProjectilePrefab()
        {
            return _weaponData?.projectilePrefab;
        }

        public void ReduceCurrentCoolDownValue(float value)
        {
            if (!IsInitialized)
            {
                return;
            }
            
            _weaponData.currentCoolDownTime -= value;
            if (_weaponData.currentCoolDownTime <= 0f)
            {
                CooledDown?.Invoke(this, EventArgs.Empty);
                ResetCurrentCoolDownValue();
            }
        }

        private void ResetCurrentCoolDownValue()
        {
            if (!IsInitialized)
            {
                return;
            }
            
            _weaponData.currentCoolDownTime = _weaponData.coolDownTime;
        }
    }
}
