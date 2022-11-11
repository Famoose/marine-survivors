using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    [RequireComponent(typeof(LevelFeature))]
    [RequireComponent(typeof(GameStateFeature))]
    public class UpLevelingBehaviour : MonoBehaviour
    {
        [SerializeField] private LevelFeature levelFeature;
        [SerializeField] private GameStateFeature gameStateFeature;
        private GameObject _levelUpMenu;

        public void Start()
        {
            _levelUpMenu = transform.Find("LevelUpMenuCanvas").gameObject;
            _levelUpMenu.SetActive(false);
            
            levelFeature.onLevelChange.AddListener(level => HandleLevelUp());
        }

        private void HandleLevelUp()
        {
            // Pause game and show level up menu
            gameStateFeature.PauseGame();
            _levelUpMenu.SetActive(true);
        }
    }
}