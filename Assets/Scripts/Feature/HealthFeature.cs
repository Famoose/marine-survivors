using System;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class HealthFeature : MonoBehaviour
    {
        [SerializeField] private HealthData initialData;
        private HealthData _healthData;
        public UnityEvent<float> onHealthChange;
        public UnityEvent onDeath;
        public bool IsInitialized { get; private set; }
        
        public void Initialize(HealthData healthData)
        {
            if (healthData == null)
            {
                throw new ArgumentException("healthData was null");
            }
            _healthData = ScriptableObject.CreateInstance<HealthData>();
            _healthData.health = healthData.health;
            _healthData.maxHealth = healthData.maxHealth;
            _healthData.shouldDestroyOnDeath = healthData.shouldDestroyOnDeath;

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }
        
        public float? GetHealth()
        {
            if (!IsInitialized)
            {
                return null;
            }
            return _healthData.health;
        }
        
        public bool? ShouldDestroyOnDeath()
        {
            if (!IsInitialized)
            {
                return null;
            }
            return _healthData.shouldDestroyOnDeath;
        }
        
        public float? GetMaxHealth()
        {
            if (!IsInitialized)
            {
                return null;
            }
            return _healthData.maxHealth;
        }

        public void AddHealth(float value)
        {
            if (!IsInitialized)
            {
                return;
            }
            _healthData.health += value;
            _healthData.health = Mathf.Clamp(_healthData.health, 0, _healthData.maxHealth);
            CheckForDeath();
            onHealthChange.Invoke(_healthData.health);
        }

        public void ReduceHealth(float value)
        {
            if (!IsInitialized)
            {
                return;
            }
            _healthData.health -= value;
            CheckForDeath();
            onHealthChange.Invoke(_healthData.health);
        }

        public void SetMaxHealth(float value)
        {
            if (!IsInitialized)
            {
                return;
            }
            _healthData.maxHealth = value;
            onHealthChange.Invoke(_healthData.health);
        }

        private void CheckForDeath()
        {
            if (_healthData.health <= 0)
            {
                _healthData.health = 0;
                onDeath.Invoke();
            }
        }
    }
}