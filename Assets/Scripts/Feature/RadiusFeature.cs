using System;
using Data;
using UnityEngine;

namespace Feature
{
    public class RadiusFeature : MonoBehaviour
    {
        [SerializeField] private RadiusData initialData;
        private RadiusData _data;
        public bool IsInitialized { get; private set; }

        public void Initialize(RadiusData radiusData)
        {
            if (radiusData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<RadiusData>();
            _data.radius = radiusData.radius;

            IsInitialized = true;
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        public float GetRadius()
        {
            return _data.radius;
        }
    }
}