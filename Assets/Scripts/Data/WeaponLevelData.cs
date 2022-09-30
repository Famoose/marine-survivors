using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WeaponLevelData", menuName = "FeatureData/WeaponLevelData", order = 1)]
    public class WeaponLevelData : ScriptableObject
    {
        public string weaponName;
        public List<WeaponData> perLevelWeaponData;
        
        [Serializable]
        public class WeaponData
        {
            public float coolDownTime;
            public float currentCoolDownTime;
            public GameObject prefab;
            public GameObject projectilePrefab;
        }
    }
}