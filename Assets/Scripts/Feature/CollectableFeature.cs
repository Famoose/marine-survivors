using System;
using Data;
using UnityEngine;

namespace Feature
{
    public class CollectableFeature : MonoBehaviour
    {
        [SerializeField] private CollectableDate initialData;
        private CollectableDate _data;
        public bool IsInitialized { get; private set; }

        public void Initialize(CollectableDate collectableDate)
        {
            if (collectableDate == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<CollectableDate>();
            _data.collectableType = collectableDate.collectableType;

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public CollectableType GetCollectableType()
        {
            if (IsInitialized)
            {
                return _data.collectableType;
            }
            throw new ArgumentException("feature data was not initialized");
        }
    }
}