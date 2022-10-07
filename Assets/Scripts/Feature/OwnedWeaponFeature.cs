using System;
using System.Linq;
using Data;
using UnityEngine;

namespace Feature
{
    public class OwnedWeaponFeature : MonoBehaviour
    {
        [SerializeField] private WeaponLevelData initialData;
        private WeaponLevelData _weaponLevelData;
        private WeaponLevelData.WeaponData _weaponData;
        
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
        }

        public WeaponLevelData.WeaponData GetCurrentLevelData()
        {
            return _weaponData;
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
    }
}