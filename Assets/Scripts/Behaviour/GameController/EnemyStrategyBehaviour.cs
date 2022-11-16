using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Enum;
using Feature;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class EnemyStrategyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyWaveFeature enemyWaveFeature;
        [SerializeField] private TrackingFeature trackingFeature;
        [SerializeField] private EnemyObserverFeature enemyObserverFeature;
        [SerializeField] private EnemyCollectionFeature enemyCollectionFeature;
        [SerializeField] private EnemyStrategyFeature enemyStrategyFeature;
        [SerializeField] private GameStateFeature gameStateFeature;

        private MovementFeature _playerMovementFeature;
        private EnemyStrategyData _enemyStrategyData;

        private void Awake()
        {
            if (enemyWaveFeature == null)
            {
                throw new ArgumentException("No enemyWaveFeature is defined");
            }

            if (trackingFeature == null)
            {
                throw new ArgumentException("No playerTrackingFeature is defined");
            }

            if (enemyObserverFeature == null)
            {
                throw new ArgumentException("No enemyObserverFeature is defined");
            }

            if (enemyCollectionFeature == null)
            {
                throw new ArgumentException("No enemyCollectionFeature is defined");
            }

            if (enemyStrategyFeature == null)
            {
                throw new ArgumentException("No enemyStrategyFeature is defined");
            }
            
            if (gameStateFeature == null)
            {
                throw new ArgumentException("No gameStateFeature is defined");
            }
        }

        private void Start()
        {
            _playerMovementFeature = trackingFeature.GetTarget().GetComponent<MovementFeature>();
            if (_playerMovementFeature == null)
            {
                throw new ArgumentException("No playerMovementFeature is defined");
            }

            _enemyStrategyData = enemyStrategyFeature.GetStrategyData();
            _enemyStrategyData.lastStrategyEvaluation = Time.time;
        }

        private void Update()
        {
            
            if (Time.time > _enemyStrategyData.lastStrategyEvaluation + _enemyStrategyData.strategySleep && !gameStateFeature.IsGamePaused())
            {
                List<EnemyConfig> newEnemies = new List<EnemyConfig>();
                int currentActiveEnemies = enemyObserverFeature.GetAllActiveEnemies().Count;
                Vector2 playerMovementDirection = _playerMovementFeature.GetMovementData().movement;
                EnemyConfig boss = CheckForBoss(playerMovementDirection, Time.time, _enemyStrategyData.baseDifficulty);
                if (boss != null)
                {
                    newEnemies.Add(boss);
                }

                EnemyConfig normals = CheckForEnemies(currentActiveEnemies, playerMovementDirection, Time.time,
                    _enemyStrategyData.baseDifficulty);
                if (normals != null)
                {
                    newEnemies.Add(normals);
                }

                enemyWaveFeature.AddWaves(newEnemies);
                _enemyStrategyData.lastStrategyEvaluation = Time.time;
            }
        }

        [CanBeNull]
        private EnemyConfig CheckForBoss(Vector2 playerMovementDirection, float time, float difficulty)
        {
            BossConfig bossConfig = _enemyStrategyData.bossConfig;
            if (time > bossConfig.lastBossSpawn + bossConfig.bossSpawnOffset)
            {
                GameObject enemyPrefab = enemyCollectionFeature.GetRandomEnemy().enemy;
                float healthMultiplier = CalculateHealthMultiplier(time, difficulty) * bossConfig.bossHealthMultiplier;
                float damageMultiplier = CalculateDamageMultiplier(time, difficulty) * bossConfig.bossDamageMultiplier;
                float expMultiplier = CalculateExpMultiplier(time, difficulty) * bossConfig.bossExpMultiplier;

                Loot pearl = CreatePearlLoot(expMultiplier);
                Loot[] bossLoot = new[]
                {
                    pearl,
                    new Loot()
                    {
                        prefab = _enemyStrategyData.collectables.chest,
                        probability = 1
                    }
                };

                HealthData healthData = CreateHealthData(healthMultiplier);
                MovementData movementData = CreateMovementData(_enemyStrategyData.walkSpeed, true);
                LootData lootData = CreateLootData(bossLoot);
                InflictDamageData inflictDamage = CreateInflictDamageData(damageMultiplier);

                bossConfig.lastBossSpawn = time;
                return new EnemyConfig
                {
                    movementOverride = movementData,
                    inflictDamageOverride = inflictDamage,
                    lootOverride = lootData,
                    amount = 1,
                    enemy = enemyPrefab,
                    timeOffset = 0,
                    positionOffset = 20,
                    healthData = healthData,
                    direction = playerMovementDirection,
                    sizeMultiplier = new Vector2(2,2)
                };
            }

            return null;
        }

        [CanBeNull]
        private EnemyConfig CheckForEnemies(int currentActive, Vector2 playerMovementDirection, float time,
            float difficulty)
        {
            var amountMultiplier = CalculateAmountMultiplier(time, difficulty);
            float desiredAmountOfEnemies = _enemyStrategyData.amountFunction.constant * amountMultiplier;
            float amountToSapwn = desiredAmountOfEnemies - currentActive;
            Debug.Log("AmountToSpawn: " + amountToSapwn);
            Debug.Log("DesiredAmountOfEnemies: " + desiredAmountOfEnemies);
            if (amountToSapwn > _enemyStrategyData.spawnThreshold)
            {
                GameObject enemyPrefab = enemyCollectionFeature.GetRandomEnemy().enemy;
                float healthMultiplier = CalculateHealthMultiplier(time, difficulty);
                float damageMultiplier = CalculateDamageMultiplier(time, difficulty);
                float expMultiplier = CalculateExpMultiplier(time, difficulty);
                Debug.Log("healthMultiplier: " + healthMultiplier);
                Debug.Log("damageMultiplier: " + damageMultiplier);
                Debug.Log("expMultiplier: " + expMultiplier);

                bool shouldFly = ShouldBeFlying(_enemyStrategyData.flyProbability);
                bool shouldCutDirection = ShouldCutDirection(_enemyStrategyData.cutDirectionProbability);
                Loot pearl = CreatePearlLoot(expMultiplier);
                List<Loot> enemyLoot = new List<Loot>();
                enemyLoot.Add(pearl);
                foreach (var collectablesItem in _enemyStrategyData.collectables.items)
                {
                    enemyLoot.Add(new Loot
                    {
                        probability = _enemyStrategyData.itemDropBaseProbability,
                        prefab = collectablesItem
                    });
                }

                HealthData healthData = CreateHealthData(healthMultiplier);
                MovementData movementData;
                if (shouldFly)
                {
                    movementData = CreateMovementData(_enemyStrategyData.flySpeed, false);
                }
                else
                {
                    movementData = CreateMovementData(_enemyStrategyData.walkSpeed, true);
                }

                Vector2 direction = Vector2.zero;
                if (shouldCutDirection)
                {
                    direction = playerMovementDirection;
                }

                LootData lootData = CreateLootData(enemyLoot.ToArray());
                InflictDamageData inflictDamage = CreateInflictDamageData(damageMultiplier);

                return new EnemyConfig
                {
                    movementOverride = movementData,
                    inflictDamageOverride = inflictDamage,
                    lootOverride = lootData,
                    amount = amountToSapwn,
                    enemy = enemyPrefab,
                    timeOffset = 0,
                    positionOffset = 20,
                    healthData = healthData,
                    direction = direction
                };
            }

            return null;
        }

        private float CalculateHealthMultiplier(float time, float difficulty)
        {
            //difficulty influence the beginning steepness
            //the healthBaseMultiplier scales the graph
            float x = (time / 60) + 1;
            return _enemyStrategyData.healthFunction.multiplier * difficulty * (float) Math.Log(x) + 1;
        }

        private float CalculateDamageMultiplier(float time, float difficulty)
        {
            //difficulty influence the beginning steepness
            //the damageBaseMultiplier scales the graph
            float x = (time / 60) + 1;
            return _enemyStrategyData.damageFunction.multiplier * difficulty * (float) Math.Log(x) + 1;
        }

        private float CalculateExpMultiplier(float time, float difficulty)
        {
            //difficulty influence the beginning steepness
            //the expBaseMultiplier scales the graph
            float x = (time / 60) + 1;
            return _enemyStrategyData.expFunction.multiplier * difficulty * (float) Math.Log(x) + 1;
        }

        private float CalculateAmountMultiplier(float time, float difficulty)
        {
            //difficulty influence the beginning steepness
            //the amountBaseMultiplier scales the graph
            float x = (time / 60) + 1;
            return _enemyStrategyData.amountFunction.multiplier * difficulty * (float) Math.Log(x) + 1;
        }

        private HealthData CreateHealthData(float multiplier)
        {
            var healthData = ScriptableObject.CreateInstance<HealthData>();
            healthData.health = _enemyStrategyData.healthFunction.constant * multiplier;
            return healthData;
        }

        private bool ShouldBeFlying(float probability)
        {
            return Random.value < probability;
        }
        
        private bool ShouldCutDirection(float probability)
        {
            return Random.value < probability;
        }

        private LootData CreateLootData(Loot[] loot)
        {
            var lootData = ScriptableObject.CreateInstance<LootData>();
            lootData.lootTable = loot;
            return lootData;
        }

        private MovementData CreateMovementData(float speed, bool shouldFollow)
        {
            var movement = ScriptableObject.CreateInstance<MovementData>();

            movement.speed = speed;
            if (shouldFollow)
            {
                movement.movementType = MovementType.FollowTarget;
            }
            else
            {
                movement.movementType = MovementType.Constant;
            }

            return movement;
        }

        private InflictDamageData CreateInflictDamageData(float multiplier)
        {
            var inflictDamage = ScriptableObject.CreateInstance<InflictDamageData>();
            inflictDamage.inflictedDamage = _enemyStrategyData.damageFunction.constant * multiplier;
            inflictDamage.ignoredGameObjectType = ActiveGameObjectType.Enemy;
            return inflictDamage;
        }

        private Loot CreatePearlLoot(float exp)
        {
            var levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.experiencePointsTotal = exp;
            //get the pearl game object depending on the exp threshold (they have other visuals)
            var pearl = _enemyStrategyData.collectables.pearls.ToList().Where(pt => pt.threshold <= exp)
                .OrderBy(pt => pt.threshold).Last();

            return new Loot
            {
                probability = 1,
                prefab = pearl.pearl,
                overrideData = levelData
            };
        }
    }
}