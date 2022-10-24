using UnityEngine;

namespace Feature
{
    public class TrackingFeature : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        public GameObject GetTarget()
        {
            return target;
        }

        public void SetTarget(GameObject o)
        {
            target = o;
        }
    }
}