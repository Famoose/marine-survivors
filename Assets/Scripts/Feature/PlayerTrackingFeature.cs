using System;
using UnityEngine;

namespace Feature
{
    public class PlayerTrackingFeature : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        public GameObject GetPlayer()
        {
            return player;
        }

        public void SetPlayer(GameObject o)
        {
            player = o;
        }
    }
}