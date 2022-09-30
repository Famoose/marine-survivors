using System;
using Data;
using UnityEngine;

namespace Feature
{
    public class ReducibleFeature : MonoBehaviour
    {
        [SerializeField] private ReducibleData initialData;
        private ReducibleData _reducibleData;

        public event EventHandler<EventArgs> ValueReducedBelowZero;
        
        private void Awake()
        {
            if (initialData == null)
            {
                throw new ArgumentException("initialData was null");
            }

            _reducibleData = ScriptableObject.CreateInstance<ReducibleData>();
            _reducibleData.value = initialData.value;
        }

        public void ReduceValue(float amount)
        {
            _reducibleData.value -= amount;
            
            if (_reducibleData.value <= 0f)
            {
                ValueReducedBelowZero?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}