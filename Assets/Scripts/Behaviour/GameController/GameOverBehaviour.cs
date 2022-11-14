using System;
using Feature;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Behaviour
{
    public class GameOverBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyCountFeature enemyCountFeature;
        [SerializeField] private CountDownFeature countDownFeature;
        [SerializeField] private TrackingFeature trackingFeature;
        [SerializeField] private GameStateFeature gameStateFeature;
        [SerializeField] private Canvas gameOverUI;
        [SerializeField] private Button menuBack;
        [SerializeField] private TextMeshProUGUI reportText;
        [FormerlySerializedAs("MainMenu")] [SerializeField] private String mainMenu;

        private void Awake()
        {
            if (enemyCountFeature == null)
            {
                throw new ArgumentException("No enemyCountFeature is defined");
            }

            if (countDownFeature == null)
            {
                throw new ArgumentException("No countDownFeature is defined");
            }
            
            if (trackingFeature == null)
            {
                throw new ArgumentException("No trackingFeature is defined");
            }
            
            if (gameStateFeature == null)
            {
                throw new ArgumentException("No gameStateFeature is defined");
            }
        }

        private void Start()
        {
            gameOverUI.gameObject.SetActive(false);
            countDownFeature.onTimeUp.AddListener(EndGame);
            GameObject target = trackingFeature.GetTarget();
            HealthFeature healthFeature = target.GetComponent<HealthFeature>();
            if (healthFeature)
            {
                healthFeature.onDeath.AddListener(EndGame);
            }
        }

        private void EndGame()
        {
            gameStateFeature.PauseGame();
            
            reportText.text = String.Format("You killed {0} enemies", enemyCountFeature.GetEnemyKilled());
            menuBack.onClick.AddListener(BackToMenu);
            gameOverUI.gameObject.SetActive(true);
        }
        
        void BackToMenu()
        {
            SceneManager.LoadScene(mainMenu);
        }
    }
}