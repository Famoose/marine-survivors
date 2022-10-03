using System;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class LevelFeature : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        public UnityEvent<int> onLevelChange;
        public UnityEvent onExperienceChange;

        private void Awake()
        {
            if (levelData == null)
            {
                throw new ArgumentException("levelData was null");
            }
        }

        public int GetLevel()
        {
            return levelData.currentLevel;
        }
        
        public float GetLevelProgress()
        {
            return levelData.experiencePointsCurrentLevel;
        }
        
        public double GetLevelProgressPercent()
        {
            return levelData.experiencePointsCurrentLevel / CalculateExperienceForNextLevel();
        }

        public void AddExperience(float value)
        {
            //add to total experience
            levelData.experiencePointsTotal += value;
            
            //calculate current level experience
            double nextLevelAmount = CalculateExperienceForNextLevel();
            levelData.experiencePointsCurrentLevel += value;
            
            //if new level reached
            if ( levelData.experiencePointsCurrentLevel >= nextLevelAmount)
            {
                levelData.currentLevel += 1;
                levelData.experiencePointsCurrentLevel = (float) (levelData.experiencePointsCurrentLevel - nextLevelAmount);
                onLevelChange.Invoke(levelData.currentLevel);
            }
            onExperienceChange.Invoke();
        }

        private double CalculateExperienceForNextLevel()
        {
            return levelData.levelCalcParam * Math.Log(levelData.currentLevel) + levelData.levelCalcConstant;
        }
    }
}