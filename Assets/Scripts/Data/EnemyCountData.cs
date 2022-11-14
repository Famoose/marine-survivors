using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemyCountData", menuName = "FeatureData/EnemyCountData", order = 0)]
    public class EnemyCountData : ScriptableObject
    {
        public int enemyKilled = 0;
    }
}