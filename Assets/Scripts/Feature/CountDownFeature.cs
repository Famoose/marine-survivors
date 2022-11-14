using System;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Feature
{
    public class CountDownFeature : MonoBehaviour
    {
        [SerializeField] private CountDownData initialData;
        private CountDownData _data;

        public UnityEvent<float> onTimerUpdate;
        public UnityEvent onTimeUp;
        public bool IsInitialized { get; private set; }

        public void Initialize(CountDownData countDownData)
        {
            if (countDownData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<CountDownData>();
            _data.timer = countDownData.timer;
            _data.timeLeft = countDownData.timeLeft;

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public float Timer()
        {
            if (IsInitialized)
            {
                return _data.timer;
            }
            throw new ArgumentException("feature data was not initialized");
        }
        
        public float GetTimeLeft()
        {
            if (IsInitialized)
            {
                return _data.timeLeft;
            }
            throw new ArgumentException("feature data was not initialized");
        }

        public void SetTimeLeft(float value)
        {
            if (IsInitialized)
            {
                _data.timeLeft = value;
                onTimerUpdate.Invoke(_data.timeLeft);
                if (_data.timeLeft <= 0)
                {
                    onTimeUp.Invoke();
                }

                return;
            }
            throw new ArgumentException("feature data was not initialized");
        }
    }
}