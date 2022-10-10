using System;
using Data;
using Data.Enum;
using UnityEngine;

namespace Feature
{
    public class InflictDamageOnCollisionFeature : MonoBehaviour
    {
        private InflictDamageData _inflictDamageData;
        public bool IsInitialized { get; private set; }

        public void Initialize(InflictDamageData inflictDamageData)
        {
            if (inflictDamageData == null)
            {
                throw new ArgumentException("The inflict damage data is not set.");
            }
            _inflictDamageData = ScriptableObject.CreateInstance<InflictDamageData>();
            _inflictDamageData.inflictedDamage = inflictDamageData.inflictedDamage;
            _inflictDamageData.destroyOnInflictingDamage = inflictDamageData.destroyOnInflictingDamage;
            _inflictDamageData.ignoredGameObjectType = inflictDamageData.ignoredGameObjectType;
            IsInitialized = true;
        }

        public float GetDamageToInflict()
        {
            if (!IsInitialized)
            {
                return 0f;
            }
            return _inflictDamageData.inflictedDamage;
        }

        public bool ShallDestroyAfterInflictingDamage()
        {
            if (!IsInitialized)
            {
                return false;
            }
            return _inflictDamageData.destroyOnInflictingDamage;
        }

        public ActiveGameObjectType? GetTypeWhichIsIgnoredForCollision()
        {
            if (!IsInitialized)
            {
                return null;
            }
            return _inflictDamageData.ignoredGameObjectType;
        }
    }
}