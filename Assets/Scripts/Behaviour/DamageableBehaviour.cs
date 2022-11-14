using System;
using System.Collections.Generic;
using Data;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class DamageableBehaviour : MonoBehaviour
    {
        
        [SerializeField] private HealthFeature healthFeature;
        [SerializeField] private AbilityFeature abilityFeature;
        [SerializeField] private BehaviourModification damageReductionModification;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No HealthFeature is defined");
            }
        }

        private void Start()
        {
            var shouldDestroyOnDeath = healthFeature.ShouldDestroyOnDeath();
            if (shouldDestroyOnDeath.HasValue)
            {
                healthFeature.onDeath.AddListener(OnDeath);
            }
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        public void InflictDamage(float damage)
        {
            float reduction = 0;
            if (abilityFeature)
            {
                List<AbilityData> activeAbilityByModification = abilityFeature.GetActiveAbilityByModification(damageReductionModification);
                foreach (AbilityData abilityData in activeAbilityByModification)
                {
                    ValueModifier valueModifier = abilityData.GetValueModifier();
                    if (valueModifier.type == ValueModifierType.Amount)
                    {
                        reduction += valueModifier.value;
                    }
                    if (valueModifier.type == ValueModifierType.Factor)
                    {
                        reduction += valueModifier.value * damage;
                    }
                }
            }

            if (reduction < damage)
            {
                healthFeature.ReduceHealth(damage - reduction);
            }
        }
    }
}