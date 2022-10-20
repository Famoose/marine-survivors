using System;
using Data;
using Data.Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace Feature
{
    public class InflictDamageOnCollisionFeature : MonoBehaviour
    {
        [SerializeField]
        private InflictDamageData inflictDamageData;
        public bool IsInitialized { get; private set; }

        public void Initialize(InflictDamageData inflictDamageData)
        {
            if (inflictDamageData == null)
            {
                throw new ArgumentException("The inflict damage data is not set.");
            }
            this.inflictDamageData = ScriptableObject.CreateInstance<InflictDamageData>();
            this.inflictDamageData.inflictedDamage = inflictDamageData.inflictedDamage;
            this.inflictDamageData.destroyOnInflictingDamage = inflictDamageData.destroyOnInflictingDamage;
            this.inflictDamageData.ignoredGameObjectType = inflictDamageData.ignoredGameObjectType;
            IsInitialized = true;
        }

        public float GetDamageToInflict()
        {
            if (!IsInitialized)
            {
                return 0f;
            }
            return inflictDamageData.inflictedDamage;
        }

        public bool ShallDestroyAfterInflictingDamage()
        {
            if (!IsInitialized)
            {
                return false;
            }
            return inflictDamageData.destroyOnInflictingDamage;
        }

        public ActiveGameObjectType? GetTypeWhichIsIgnoredForCollision()
        {
            if (!IsInitialized)
            {
                return null;
            }
            return inflictDamageData.ignoredGameObjectType;
        }
    }
}