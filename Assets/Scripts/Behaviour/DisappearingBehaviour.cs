using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class DisappearingBehaviour : MonoBehaviour
    {
        [SerializeField] private ReducibleFeature reducibleFeature;

        public ReducibleFeature GetReducibleFeature()
        {
            return reducibleFeature;
        }
        
        private void Awake()
        {
            if (reducibleFeature == null)
            {
                throw new ArgumentException("No ReducibleFeature is defined");
            }
            
            reducibleFeature.ValueReducedBelowZero += ReducibleFeatureOnValueReducedBelowZero;
        }

        private void FixedUpdate()
        {
            reducibleFeature.ReduceValue(Time.fixedDeltaTime);
        }
        
        private void ReducibleFeatureOnValueReducedBelowZero(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}