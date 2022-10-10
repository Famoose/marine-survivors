using Data.Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "InflictDamageData", menuName = "FeatureData/InflictDamageData", order = 1)]
    public class InflictDamageData : ScriptableObject
    {
        public float inflictedDamage;
        public bool destroyOnInflictingDamage;
        public ActiveGameObjectType ignoredGameObjectType;
    }
}