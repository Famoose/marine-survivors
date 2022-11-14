using System;
using Feature;
using TMPro;
using UnityEngine;

namespace Behaviour
{
    public class CountDownBehaviour : MonoBehaviour
    {
        [SerializeField] private CountDownFeature countDownFeature;
        [SerializeField] private GameStateFeature gameStateFeature;
        [SerializeField] private TextMeshProUGUI countDownText;
        private float _interval = 1; 
        private float _lastCheck = 0;
        private void Awake()
        {
            if (countDownFeature == null)
            {
                throw new ArgumentException("No countDownFeature is defined");
            }
            
            if (gameStateFeature == null)
            {
                throw new ArgumentException("No gameStateFeature is defined");
            }
        }

        private void Start()
        {
            SetCountdownText();
        }

        private void Update()
        {
            if (_lastCheck + _interval <= Time.time && !gameStateFeature.IsGamePaused())
            {
                countDownFeature.SetTimeLeft(countDownFeature.GetTimeLeft() - _interval);
                SetCountdownText();
                _lastCheck = Time.time;
            }
        }

        private void SetCountdownText()
        {
            var time = countDownFeature.GetTimeLeft();
            countDownText.text = $"{(int) time / 60}:{time % 60:00}";
        }
    }
}