using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "FeatureData/MovementData", order = 1)]
    public class MovementData : ScriptableObject
    {
        public float playerSpeed = 10;
        public Vector2 playerMovement;
    }
}