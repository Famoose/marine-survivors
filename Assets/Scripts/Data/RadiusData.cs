using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "RadiusData", menuName = "FeatureData/RadiusData", order = 0)]
    public class RadiusData : ScriptableObject
    {
        public float radius = 20;
    }
}