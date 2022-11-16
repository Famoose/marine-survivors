using Feature;
using UnityEngine;

namespace Behaviour
{
    [RequireComponent(typeof(LevelFeature))]
    [RequireComponent(typeof(GameStateFeature))]
    [RequireComponent(typeof(OwnedWeaponFeature))]
    [RequireComponent(typeof(AbilityFeature))]
    public class UpLevelingBehaviour : MonoBehaviour
    {
        [SerializeField] private LevelFeature levelFeature;
        [SerializeField] private GameStateFeature gameStateFeature;
        [SerializeField] private OwnedWeaponFeature ownedWeaponFeature;
        [SerializeField] private AbilityFeature abilityFeature;
        private GameObject _levelUpMenu;

        public void Start()
        {
            _levelUpMenu = transform.Find("LevelUpMenuCanvas").gameObject;
            _levelUpMenu.SetActive(false);
            
            levelFeature.onLevelChange.AddListener(level => HandleLevelUp());
        }

        private void HandleLevelUp()
        {
            _levelUpMenu.GetComponent<LevelUpMenuBehaviour>()
                .PrepareMenu(ownedWeaponFeature, abilityFeature);
            
            // Pause game and show level up menu
            gameStateFeature.PauseGame();
            _levelUpMenu.SetActive(true);
        }
    }
}