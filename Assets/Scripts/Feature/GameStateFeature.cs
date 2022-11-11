using System;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class GameStateFeature : MonoBehaviour
    {
        [SerializeField] private GameStateData data;
        public UnityEvent onGamePaused;
        public UnityEvent onGameUnpaused;

        private void Awake()
        {
            if (data == null)
            {
                throw new ArgumentException("no initial data provided");
            }
        }

        public void PauseGame()
        {
            // Slow done or stop all animations and movements, which are based on time delta
            Time.timeScale = data.timeScaleIfPaused;
            data.currentTimeScale = Time.timeScale;
            data.isGamePaused = true;
            
            onGamePaused.Invoke();
        }

        public void UnpauseGame()
        {
            // Set the time scale back to default to make animations and movements based on time delta running again
            Time.timeScale = data.timeScaleIfUnpaused;
            data.currentTimeScale = Time.timeScale;
            data.isGamePaused = false;
            
            onGameUnpaused.Invoke();
        }

        public bool IsGamePaused()
        {
            return data.isGamePaused;
        }
    }
}