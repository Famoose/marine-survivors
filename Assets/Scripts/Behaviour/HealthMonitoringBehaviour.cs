using System;
using Data;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class HealthMonitoringBehaviour : MonoBehaviour
    {
        [SerializeField] private HealthFeature healthFeature;
        [SerializeField] private AbilityFeature abilityFeature;
        [SerializeField] private BehaviourModification healthBehaviourModification;
        private float? initialMaxHealth;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No HealthFeature is defined");
            }
            if (abilityFeature == null)
            {
                throw new ArgumentException("No AbilityFeature is defined");
            }
        }

        private void Start()
        {
            this.abilityFeature.onAbilityActivated.AddListener(SetMaxHealth);
            this.abilityFeature.onAbilityLevelUp.AddListener(SetMaxHealth);
            initialMaxHealth = healthFeature.GetMaxHealth();
        }

        private void SetMaxHealth(AbilityData abilityData)
        {
            if (abilityData.behaviourModification == healthBehaviourModification)
            {
                if (initialMaxHealth.HasValue)
                {
                    float additionalHealth = 0;
                    ValueModifier valueModifier = abilityData.GetValueModifier();
                    if (valueModifier.type == ValueModifierType.Amount)
                    {
                        additionalHealth += valueModifier.value;
                    }
                    if (valueModifier.type == ValueModifierType.Factor)
                    {
                        additionalHealth += valueModifier.value * initialMaxHealth.Value;
                    }
                    healthFeature.SetMaxHealth(initialMaxHealth.Value + additionalHealth);
                }
            }
        }
    }
}