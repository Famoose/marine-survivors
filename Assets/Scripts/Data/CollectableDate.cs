using UnityEngine;

namespace Data
{
    
    public enum CollectableType
    {
        Pearl,
        Chest,
        Item
    }
    [CreateAssetMenu(fileName = "CollectableDate", menuName = "FeatureData/CollectableDate", order = 0)]
    public class CollectableDate : ScriptableObject
    {
        public CollectableType collectableType;
    }
}