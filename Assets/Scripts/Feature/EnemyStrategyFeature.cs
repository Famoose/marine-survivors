using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class EnemyStrategyFeature : MonoBehaviour
    {
        [SerializeField] private EnemyStrategyData initialData;
        private EnemyStrategyData _data;
        public UnityEvent onEnemyDeleted;

        public bool IsInitialized { get; private set; }

        public void Initialize(EnemyStrategyData strategyData)
        {
            if (strategyData == null)
            {
                throw new ArgumentException("initialData was null");
            }

            _data = ScriptableObject.CreateInstance<EnemyStrategyData>();
           _data.bossConfig = strategyData.bossConfig.Copy();
           _data.collectables = strategyData.collectables.Copy();
           _data.healthFunction = strategyData.healthFunction.Copy();
           _data.expFunction = strategyData.expFunction.Copy();
           _data.damageFunction = strategyData.damageFunction.Copy();
           _data.amountFunction = strategyData.amountFunction.Copy();
           _data.walkSpeed = strategyData.walkSpeed;
           _data.flySpeed = strategyData.flySpeed;
           _data.strategySleep = strategyData.strategySleep;
           _data.lastStrategyEvaluation = strategyData.lastStrategyEvaluation;
           _data.flyProbability = strategyData.flyProbability;
           _data.cutDirectionProbability = strategyData.cutDirectionProbability;
           _data.itemDropBaseProbability = strategyData.itemDropBaseProbability;
           _data.spawnThreshold = strategyData.spawnThreshold;

            IsInitialized = true;
        }

        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public EnemyStrategyData GetStrategyData()
        {
            if (IsInitialized)
            {
                return _data;
            }
            throw new ArgumentException("data is not initialized");
        }
    }
}