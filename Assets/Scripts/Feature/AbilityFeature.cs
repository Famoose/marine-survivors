using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class AbilityFeature : MonoBehaviour
    {
        [SerializeField] private List<AbilityData> activeAbilities;
        [SerializeField] private List<AbilityData> availableAbilities;
        [SerializeField] private int maximumAbilities = 6;
        public UnityEvent<AbilityData> onAbilityLevelUp;
        public UnityEvent<AbilityData> onAbilityActivated;

        public AbilityData GetActiveAbilityByModification(BehaviourModification modification)
        {
            return activeAbilities.Find(data => data.behaviourModification == modification);
        }

        public List<AbilityData> GetActiveAbilities()
        {
            return activeAbilities;
        }

        public List<AbilityData> GetAvailableAbilities()
        {
            return activeAbilities;
        }

        public void LevelUpAbility(AbilityData ability)
        {
            AbilityData abilityData = activeAbilities.Find(data => data.Equals(ability));
            if (abilityData)
            {
                abilityData.IncreaseLevel();
                onAbilityLevelUp.Invoke(abilityData);
            }
            else
            {
                throw new ArgumentException("Ability is not active");
            }
        }

        public void ActivateAbility(AbilityData ability)
        {
            if (activeAbilities.Count < maximumAbilities)
            {
                AbilityData abilityData = availableAbilities.Find(data => data.Equals(ability));
                if (abilityData)
                {
                    activeAbilities.Add(abilityData);
                    availableAbilities.Remove(abilityData);
                    onAbilityActivated.Invoke(abilityData);
                }
                else
                {
                    throw new ArgumentException("Ability is not available");
                }
            }
            else
            {
                throw new ArgumentException("Maximum abilities active reached");
            }
        }
    }
}