using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CountDownDate", menuName = "FeatureData/CountDownDate", order = 0)]
    public class CountDownData : ScriptableObject
    {
        public float timer = 150;
        public float timeLeft = 150;
    }
}