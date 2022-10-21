using System;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class LevelFeature : MonoBehaviour
    {
        [SerializeField] private LevelData initialData;
        private LevelData _data;
        public UnityEvent<int> onLevelChange;
        public UnityEvent onExperienceChange;
        
        public bool IsInitialized { get; private set; }

        public void Initialize(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<LevelData>();
            _data.levelCalcConstant = levelData.levelCalcConstant;
            _data.levelCalcParam = levelData.levelCalcParam;
            _data.currentLevel = levelData.currentLevel;
            _data.experiencePointsTotal = levelData.experiencePointsTotal;
            _data.experiencePointsCurrentLevel = levelData.experiencePointsCurrentLevel;

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public int GetLevel()
        {
            return _data.currentLevel;
        }
        
        public float GetLevelProgress()
        {
            return _data.experiencePointsCurrentLevel;
        }
        
        public float GetExperiencePointsTotal()
        {
            return _data.experiencePointsTotal;
        }
        
        public double GetLevelProgressPercent()
        {
            return _data.experiencePointsCurrentLevel / CalculateExperienceForNextLevel();
        }

        public void AddExperience(float value)
        {
            //add to total experience
            _data.experiencePointsTotal += value;
            
            //calculate current level experience
            double nextLevelAmount = CalculateExperienceForNextLevel();
            _data.experiencePointsCurrentLevel += value;
            
            //if new level reached
            if ( _data.experiencePointsCurrentLevel >= nextLevelAmount)
            {
                _data.currentLevel += 1;
                _data.experiencePointsCurrentLevel = (float) (_data.experiencePointsCurrentLevel - nextLevelAmount);
                onLevelChange.Invoke(_data.currentLevel);
            }
            onExperienceChange.Invoke();
        }

        private double CalculateExperienceForNextLevel()
        {
            return _data.levelCalcParam * Math.Log(_data.currentLevel) + _data.levelCalcConstant;
        }
    }
}