using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Awake()
        {
            activeAbilities = activeAbilities.Select(InitializeAbility).ToList();
            availableAbilities = availableAbilities.Select(InitializeAbility).ToList();
        }

        private AbilityData InitializeAbility(AbilityData initialData)
        {
            if (initialData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            AbilityData abilityData = ScriptableObject.CreateInstance<AbilityData>();
            abilityData.abilityName = initialData.abilityName;
            abilityData.behaviourModification = initialData.behaviourModification;
            abilityData.valueModifiers = initialData.valueModifiers.Select(vm => vm.Copy()).ToList();

            return abilityData;
        }

        private void Start()
        {
            foreach (AbilityData abilityData in activeAbilities)
            {
                onAbilityActivated.Invoke(abilityData);
            }
        }
        
        public List<AbilityData> GetActiveAbilityByModification(BehaviourModification modification)
        {
            return activeAbilities.FindAll(data => data.behaviourModification == modification);
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