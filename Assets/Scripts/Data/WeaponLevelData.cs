using System;
using System.Collections;
using System.Collections.Generic;
using Data.Enum;
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
            public float projectileSpeed;
            public MovementType projectileMovementType;
            public InitialMovementType projectileInitialMovementType;
            public Vector2 projectileInitialMovementDirection;
            public float projectileLifetime;
            public GameObject prefab;
            public GameObject projectilePrefab;
        }
    }
}