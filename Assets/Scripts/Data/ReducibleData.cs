using Data.Enum;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ReducibleData", menuName = "FeatureData/ReducibleData", order = 1)]
    public class ReducibleData : ScriptableObject
    {
        public float value;
    }
}