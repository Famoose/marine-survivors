using System;
using Data;
using UnityEngine;

namespace Feature
{
    public class ReducibleFeature : MonoBehaviour
    {
        private ReducibleData _reducibleData;
        public bool IsInitialized { get; private set; }

        public event EventHandler<EventArgs> ValueReducedBelowZero;

        public void Initialize(ReducibleData reducibleData)
        {
            if (reducibleData == null)
            {
                throw new ArgumentException("The reducible data is not set.");
            }
            _reducibleData = ScriptableObject.CreateInstance<ReducibleData>();
            _reducibleData.value = reducibleData.value;
            IsInitialized = true;
        }

        public void ReduceValue(float amount)
        {
            if (!IsInitialized)
            {
                return;
            }
            
            _reducibleData.value -= amount;
            
            if (_reducibleData.value <= 0f)
            {
                ValueReducedBelowZero?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}