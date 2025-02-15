using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "HealthData", menuName = "FeatureData/HealthData", order = 1)]
    public class HealthData: ScriptableObject
    {
        public float health = 100;
        public float maxHealth = 100;
        public bool shouldDestroyOnDeath = true;
        public bool isDead = false;
    }
}