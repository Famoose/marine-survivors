using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Slider difficultySlider;
    [SerializeField] private TextMeshProUGUI difficultyValue;
    [SerializeField] private Button startButton;
    [SerializeField] private EnemyStrategyData enemyStrategyData;
    [SerializeField] private String levelName;
    
    void Start()
    {
        SetDifficultyValue(difficultySlider.value);
        difficultySlider.onValueChanged.AddListener(SetDifficultyValue);
        startButton.onClick.AddListener(StartGame);
    }

    void SetDifficultyValue(float value)
    {
        difficultyValue.text = String.Format("{0:0.00}", value);
        enemyStrategyData.baseDifficulty = value;
        difficultySlider.value = value;
    }

    void StartGame()
    {
        SceneManager.LoadScene(levelName);
    }
    
}
