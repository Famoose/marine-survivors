using System;
using Data.Enum;
using UnityEngine;

namespace Data
{
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