using System;
using System.Linq;
using Data;
using UnityEngine;

namespace Feature
{
    public class WeaponFeature : MonoBehaviour
    {
        [SerializeField] private WeaponLevelData initialData;
        private WeaponLevelData _weaponLevelData;
        private WeaponLevelData.WeaponData _weaponData;

        public event EventHandler<EventArgs> CooledDown;

        private void Awake()
        {
            if (initialData == null)
            {
                throw new ArgumentException("initialData was null");
            }

            if (initialData.perLevelWeaponData == null || !initialData.perLevelWeaponData.Any())
            {
                throw new ArgumentException("The weapon does not contain any levels");
            }
            _weaponLevelData = ScriptableObject.CreateInstance<WeaponLevelData>();
            _weaponLevelData.weaponName = initialData.weaponName;

            SetWeaponLevel(0);
            
            ResetCurrentCoolDownValue();
        }

        public GameObject GetCurrentLevelPrefab()
        {
            return _weaponData.prefab;
        }

        public void SetWeaponLevel(int level)
        {
            _weaponData = initialData.perLevelWeaponData[level];
        }

        public int GetMaxWeaponLevel()
        {
            return initialData.perLevelWeaponData.Count - 1;
        }

        public float GetCurrentCoolDownValue()
        {
            return _weaponData.currentCoolDownTime;
        }
        
        public void ReduceCurrentCoolDownValue(float value)
        {
            _weaponData.currentCoolDownTime -= value;
            if (_weaponData.currentCoolDownTime <= 0f)
            {
                CooledDown?.Invoke(this, EventArgs.Empty);
                ResetCurrentCoolDownValue();
            }
        }

        private void ResetCurrentCoolDownValue()
        {
            _weaponData.currentCoolDownTime = _weaponData.coolDownTime;
        }
    }
}
