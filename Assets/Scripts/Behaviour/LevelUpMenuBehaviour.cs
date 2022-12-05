using System.Collections.Generic;
using System.Linq;
using Data;
using Feature;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    [RequireComponent(typeof(GameStateFeature))]
    public class LevelUpMenuBehaviour : MonoBehaviour
    {
        private const int MAX_OPTIONS = 4;
        
        [SerializeField] private GameStateFeature gameStateFeature;
        private OwnedWeaponFeature _ownedWeaponFeature;
        private AbilityFeature _abilityFeature;
        private List<GameObject> _chooseOptionParents;
        private List<GameObject> _optionTitleTexts;
        private List<GameObject> _optionDescTexts;
        private List<GameObject> _optionWeaponTexts;
        private List<UpgradeOption> _upgradeOptions;

        public void PrepareMenu(OwnedWeaponFeature ownedWeaponFeature, AbilityFeature abilityFeature)
        {
            _ownedWeaponFeature = ownedWeaponFeature;
            _abilityFeature = abilityFeature;

            // Get possible Upgrades
            List<WeaponLevelData> availableNewWeapons = _ownedWeaponFeature.CanActivateNewWeapon() 
                ? _ownedWeaponFeature.GetAvailableButNotActiveWeapons() 
                : new List<WeaponLevelData>();
            List<WeaponLevelData> activeNotCompletelyLeveledUpWeapons =
                _ownedWeaponFeature.GetActiveButNotCompletelyLeveledUpWeapons();
            List<AbilityData> availableNewAbilities = abilityFeature.CanActivateNewAbility() 
                ? _abilityFeature.GetAvailableButNotActiveAbilities() 
                : new List<AbilityData>();
            List<AbilityData> activeNotCompletelyLeveledUpAbilities =
                _abilityFeature.GetActiveButNotCompletelyLeveledUpAbilities();
            
            // Generate a random collection of four upgrade options
            List<UpgradeOption> possibleUpgradeOptions = new List<UpgradeOption>();
            foreach (WeaponLevelData availableNewWeapon in availableNewWeapons)
            {
                possibleUpgradeOptions.Add(new UpgradeOption(
                    UpgradeType.ActivateNewWeapon,
                    availableNewWeapon,
                    _ownedWeaponFeature,
                    _abilityFeature));
            }
            foreach (WeaponLevelData activeNotCompletelyLeveledUpWeapon in activeNotCompletelyLeveledUpWeapons)
            {
                possibleUpgradeOptions.Add(new UpgradeOption(
                    UpgradeType.UpgradeActiveWeapon,
                    activeNotCompletelyLeveledUpWeapon,
                    _ownedWeaponFeature,
                    _abilityFeature));
            }
            foreach (AbilityData availableNewAbility in availableNewAbilities)
            {
                possibleUpgradeOptions.Add(new UpgradeOption(
                    UpgradeType.ActivateNewAbility,
                    availableNewAbility,
                    _ownedWeaponFeature,
                    _abilityFeature));
            }
            foreach (AbilityData activeNotCompletelyLeveledUpAbility in activeNotCompletelyLeveledUpAbilities)
            {
                possibleUpgradeOptions.Add(new UpgradeOption(
                    UpgradeType.UpgradeActiveAbility,
                    activeNotCompletelyLeveledUpAbility,
                    _ownedWeaponFeature,
                    _abilityFeature));
            }

            _upgradeOptions = new List<UpgradeOption>();
            for (int i = 0; i < MAX_OPTIONS; i++)
            {
                if (!possibleUpgradeOptions.Any())
                {
                    // Nothing left to upgrade...
                    _chooseOptionParents[i].SetActive(false);
                    continue;
                }
                
                UpgradeOption randomOption = possibleUpgradeOptions[Random.Range(0, possibleUpgradeOptions.Count)];
                possibleUpgradeOptions.Remove(randomOption);
                _upgradeOptions.Add(randomOption);
                _chooseOptionParents[i].SetActive(true);
                _optionTitleTexts[i].GetComponent<TextMeshProUGUI>().text = randomOption.UpgradableData.GetName();
                _optionDescTexts[i].GetComponent<TextMeshProUGUI>().text =
                    randomOption.UpgradeType is UpgradeType.ActivateNewAbility or UpgradeType.ActivateNewWeapon
                        ? "New"
                        : $"Upgrade to level {randomOption.UpgradableData.GetLevel() + 2}";
                _optionWeaponTexts[i].GetComponent<TextMeshProUGUI>().text =
                    randomOption.UpgradeType is UpgradeType.ActivateNewAbility or UpgradeType.ActivateNewWeapon
                        ? randomOption.UpgradableData.GetCurrentLevelDescription()
                        : randomOption.UpgradableData.GetNextLevelDescription();
            }

            if (!_upgradeOptions.Any())
            {
                // No options left to update. Hide menu to silently level up
                HideMenuAndUnpauseGame();
            }
        }
        
        public void Start()
        {
            _chooseOptionParents = new List<GameObject>();
            _optionTitleTexts = new List<GameObject>();
            _optionDescTexts = new List<GameObject>();
            _optionWeaponTexts = new List<GameObject>();
            for (int i = 0; i < MAX_OPTIONS; i++)
            {
                GameObject parent = transform.Find($"LevelUpMenu/ChooseOption{i}").gameObject;
                GameObject button = transform.Find($"LevelUpMenu/ChooseOption{i}/ChooseOption{i}Button").gameObject;
                GameObject title = transform.Find($"LevelUpMenu/ChooseOption{i}/Option{i}TitleText").gameObject;
                GameObject description = transform.Find($"LevelUpMenu/ChooseOption{i}/Option{i}DescText").gameObject;
                GameObject weaponText = transform.Find($"LevelUpMenu/ChooseOption{i}/Option{i}WeaponText").gameObject;
                
                int index = i;
                button.GetComponent<Button>().onClick.AddListener(() => ChooseOption(index));
                _chooseOptionParents.Add(parent);
                _optionTitleTexts.Add(title);
                _optionDescTexts.Add(description);
                _optionWeaponTexts.Add(weaponText);
            }
        }

        private void ChooseOption(int optionIndex)
        {
            _upgradeOptions[optionIndex].PerformUpgrade();
            
            HideMenuAndUnpauseGame();
        }

        private void HideMenuAndUnpauseGame()
        {
            gameObject.SetActive(false);
            gameStateFeature.UnpauseGame();
        }
        
        private class UpgradeOption
        {
            private readonly OwnedWeaponFeature _ownedWeaponFeature;
            private readonly AbilityFeature _abilityFeature;
         
            public UpgradeType UpgradeType { get; }
            public IUpgradableData UpgradableData { get; }

            public UpgradeOption(
                UpgradeType upgradeType, 
                IUpgradableData upgradableData,
                OwnedWeaponFeature ownedWeaponFeature,
                AbilityFeature abilityFeature)
            {
                UpgradeType = upgradeType;
                UpgradableData = upgradableData;
                _ownedWeaponFeature = ownedWeaponFeature;
                _abilityFeature = abilityFeature;
            }

            public void PerformUpgrade()
            {
                switch (UpgradeType)
                {
                    case UpgradeType.ActivateNewWeapon:
                        _ownedWeaponFeature.ActivateWeapon(UpgradableData.GetName());
                        break;
                    case UpgradeType.ActivateNewAbility:
                        _abilityFeature.ActivateAbility(UpgradableData as AbilityData);
                        break;
                    case UpgradeType.UpgradeActiveWeapon:
                        _ownedWeaponFeature.LevelUpWeapon(UpgradableData.GetName());
                        break;
                    case UpgradeType.UpgradeActiveAbility:
                        _abilityFeature.LevelUpAbility(UpgradableData as AbilityData);
                        break;
                }
            }
        }

        private enum UpgradeType
        {
            ActivateNewWeapon,
            ActivateNewAbility,
            UpgradeActiveWeapon,
            UpgradeActiveAbility,
        }
    }
}