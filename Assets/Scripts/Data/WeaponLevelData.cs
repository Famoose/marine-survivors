using System;
using System.Collections;
using System.Collections.Generic;
using Data.Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "WeaponLevelData", menuName = "FeatureData/WeaponLevelData", order = 1)]
    public class WeaponLevelData : ScriptableObject, IUpgradableData
    {
        public string weaponName;
        public List<WeaponData> perLevelWeaponData;
        private int _level = 0;

        public string GetName()
        {
            return weaponName;
        }

        public int GetLevel()
        {
            return _level;
        }

        public void IncreaseLevel()
        {
            if (_level == GetMaxLevel())
            {
                throw new ArgumentException("The weapon is already at its highest level.");
            }
            _level++;
        }
        
        public int GetMaxLevel()
        {
            return perLevelWeaponData.Count - 1;
        }

        public WeaponData GetCurrentLevelsWeaponData()
        {
            return perLevelWeaponData[_level];
        }

        public bool IsMaxLevel()
        {
            return _level >= GetMaxLevel();
        }
    }
}