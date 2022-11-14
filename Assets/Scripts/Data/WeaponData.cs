using System;
using Data.Enum;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

namespace Data
{
    [Serializable]
    public class WeaponData
    {
        public float coolDownTime;
        public float currentCoolDownTime;
        public float projectileSpeed;
        public int projectileAmount;
        public MovementType projectileMovementType;
        public InitialMovementType projectileInitialMovementType;
        public Vector2 projectileInitialMovementDirection;
        public float projectileLifetime;
        public float projectileInflictedDamage;
        public GameObject prefab;
        public GameObject projectilePrefab;
        public Vector3 weaponPositionOffset;

        public WeaponData Copy()
        {
            return new WeaponData
            {
                coolDownTime = this.coolDownTime,
                currentCoolDownTime = this.currentCoolDownTime,
                projectileSpeed = this.projectileSpeed,
                projectileAmount = this.projectileAmount,
                projectileMovementType = this.projectileMovementType,
                projectileInitialMovementType = this.projectileInitialMovementType,
                projectileInitialMovementDirection = this.projectileInitialMovementDirection,
                projectileLifetime = this.projectileLifetime,
                projectileInflictedDamage = this.projectileInflictedDamage,
                prefab = this.prefab,
                projectilePrefab = this.projectilePrefab,
                weaponPositionOffset = this.weaponPositionOffset
            };
        }
    }
}