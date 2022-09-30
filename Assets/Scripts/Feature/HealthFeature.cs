using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Feature
{
    public class HealthFeature : MonoBehaviour
    {
        [SerializeField] private HealthData healthData;
        public UnityEvent<float> onHealthChange;
        public UnityEvent onDeath;

        public float GetHealth()
        {
            return healthData.health;
        }

        public void AddHealth(float value)
        {
            if (healthData.health + value <= 0)
            {
                healthData.health = 0;
                onDeath.Invoke();
            }
            else
            {
                healthData.health += value;
            }

            onHealthChange.Invoke(healthData.health);
        }
    }
}