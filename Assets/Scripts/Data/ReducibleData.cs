using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ReducibleData", menuName = "FeatureData/ReducibleData", order = 0)]
    public class ReducibleData : ScriptableObject
    {
        public float value;
    }
}