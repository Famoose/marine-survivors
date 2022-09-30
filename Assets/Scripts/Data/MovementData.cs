using Data.Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "FeatureData/MovementData", order = 1)]
    public class MovementData : ScriptableObject
    {
        public float speed = 10;
        public Vector2 movement;
        public MovementType movementType;
        public InitialMovementType initialMovementType;
    }
}