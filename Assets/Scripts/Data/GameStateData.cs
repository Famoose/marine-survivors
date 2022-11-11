using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameStateData", menuName = "FeatureData/GameStateData", order = 1)]
    public class GameStateData : ScriptableObject
    {
        public bool isGamePaused = false;
        public float timeScaleIfPaused = 0;
        public float timeScaleIfUnpaused = 1;
        public float currentTimeScale = 1;
    }
}