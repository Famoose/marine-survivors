using System;
using Data;
using Feature;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviour
{
    [RequireComponent(typeof(AbilityFeature))]
    [RequireComponent(typeof(LevelFeature))]
    [RequireComponent(typeof(HealthFeature))]
    public class PlayerDebugUIBehaviour : MonoBehaviour
    {
        [SerializeField] private HealthFeature healthFeature;
        [SerializeField] private LevelFeature levelFeature;
        [SerializeField] private AbilityFeature abilityFeature;

        public TextMeshProUGUI textHealth;
        public TextMeshProUGUI textLevel;
        public TextMeshProUGUI textLevelPercent;
        public TextMeshProUGUI textAbilitiesActive;
        public TextMeshProUGUI textAbilitiesAvailable;

        public void Start()
        {
            RenderDebugStats();
            
            healthFeature.onHealthChange.AddListener(health => RenderDebugStats());
            levelFeature.onExperienceChange.AddListener(RenderDebugStats);
            abilityFeature.onAbilityActivated.AddListener(ability => RenderDebugStats());
        }

        private void RenderDebugStats()
        {
            textHealth.text = String.Format("Health: {0}", this.healthFeature.GetHealth());
            textLevel.text = String.Format("Level: {0}", this.levelFeature.GetLevel());
            textLevelPercent.text = String.Format("Level%: {0:0.00}", this.levelFeature.GetLevelProgressPercent());
            textAbilitiesActive.text = String.Format("Abilities Active: {0}", this.abilityFeature.GetActiveAbilities().Count);
            textAbilitiesAvailable.text = String.Format("Abiliites Available: {0}", this.abilityFeature.GetAvailableAbilities().Count);
        }
    }
}