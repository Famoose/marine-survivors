using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class DamageableBehaviour : MonoBehaviour
    {
        [SerializeField] private HealthFeature healthFeature;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No HealthFeature is defined");
            }
            
            healthFeature.onDeath.AddListener(OnDeath);
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        public void InflictDamage(float damage)
        {
            healthFeature.ReduceHealth(damage);
        }
    }
}