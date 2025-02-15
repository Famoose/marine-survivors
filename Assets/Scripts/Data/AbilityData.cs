using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public enum ValueModifierType
    {
        Factor,
        Amount
    }

    [Serializable]
    public class ValueModifier
    {
        public string levelDescription;
        public float value;
        public ValueModifierType type;

        public ValueModifier Copy()
        {
            return new ValueModifier
            {
                levelDescription = this.levelDescription,
                value = this.value,
                type = this.type
            };
        }
    }

    [CreateAssetMenu(fileName = "AbilityData", menuName = "FeatureData/AbilityData", order = 0)]
    public class AbilityData : ScriptableObject, IUpgradableData
    {
        public BehaviourModification behaviourModification;
        [SerializeField] public List<ValueModifier> valueModifiers;
        public string abilityName;
        private int _level = 0;

        public string GetName()
        {
            return abilityName;
        }
        
        public int GetLevel()
        {
            return _level;
        }

        public string GetCurrentLevelDescription()
        {
            return GetValueModifier()?.levelDescription;
        }

        public string GetNextLevelDescription()
        {
            return IsMaxLevel()
                ? string.Empty
                : valueModifiers[_level + 1]?.levelDescription;
        }

        public void IncreaseLevel()
        {
            if (_level == GetMaxLevel())
            {
                throw new InvalidOperationException("Level is already maxed");
            }

            _level++;
        }

        public int GetMaxLevel()
        {
            return valueModifiers.Count - 1;
        }

        public ValueModifier GetValueModifier()
        {
            return valueModifiers[_level];
        }

        public bool IsMaxLevel()
        {
            return _level >= GetMaxLevel();
        }
    }
}