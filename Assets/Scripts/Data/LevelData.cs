using UnityEngine;

namespace Data
{
    
    [CreateAssetMenu(fileName = "LevelData", menuName = "FeatureData/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public int currentLevel = 1;
        public float experiencePointsCurrentLevel = 0;
        public float experiencePointsTotal = 0;
        public float levelCalcParam = 100;
        public float levelCalcConstant = 10;
    }
}