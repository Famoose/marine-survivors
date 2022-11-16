using System;
using System.Collections.Generic;
using Data;
using Feature;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    public class DamageableBehaviour : MonoBehaviour
    {
        
        [SerializeField] private HealthFeature healthFeature;
        [SerializeField] private AbilityFeature abilityFeature;
        [SerializeField] private BehaviourModification damageReductionModification;
        [SerializeField] private GameObject damageIndicator;
        private bool _hasDamageIndicator;
        private Image _damageIndicatorImage;
        private float _damageIndicatorShowTime;

        private void Awake()
        {
            if (healthFeature == null)
            {
                throw new ArgumentException("No HealthFeature is defined");
            }

            if (damageIndicator != null)
            {
                _damageIndicatorImage = damageIndicator.transform.Find("DamageIndicator").GetComponent<Image>();
                _hasDamageIndicator = _damageIndicatorImage != null;
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

        private void FixedUpdate()
        {
            if (_hasDamageIndicator)
            {
                if(_damageIndicatorShowTime > 0)
                {
                    _damageIndicatorShowTime -= Time.deltaTime;
                    _damageIndicatorImage.color = new Color(
                        _damageIndicatorImage.color.r, 
                        _damageIndicatorImage.color.g, 
                        _damageIndicatorImage.color.b,
                        _damageIndicatorImage.color.a - 0.2f * Time.deltaTime);
                }
                else
                {
                    damageIndicator.SetActive(false);
                }
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

            if (_hasDamageIndicator)
            {
                damageIndicator.SetActive(true);
                _damageIndicatorShowTime = 2f;
                _damageIndicatorImage.color = new Color(1f, 1f, 1f, 0.3f);
            }
        }
    }
}