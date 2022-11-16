using System;
using Feature;
using UnityEngine;

namespace Behaviour.item
{
    public class HealingBehaviour : MonoBehaviour, IItemBehaviour
    {
        public HealthFeature healthFeature;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No healthFeature is defined.");
            }
        }

        public void ActivateItem(GameObject activator)
        {
            HealthFeature activatorsHealthFeature = activator.GetComponent<HealthFeature>();
            float? healthToAdd = healthFeature.GetHealth();
            if (activatorsHealthFeature != null && healthToAdd.HasValue)
            {
                activatorsHealthFeature.AddHealth(healthToAdd.Value);
            }
        }
    }
}