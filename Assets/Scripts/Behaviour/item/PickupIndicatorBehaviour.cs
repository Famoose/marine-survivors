using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Behaviour.item
{
    public class PickupIndicatorBehaviour : MonoBehaviour
    {
        [SerializeField] private Light2D lightComponent;
        private bool _isGrowing = true;
        
        private void Update()
        {
            if (lightComponent.pointLightInnerRadius >= lightComponent.pointLightOuterRadius - 0.1f)
            {
                _isGrowing = false;
            }
            else if (lightComponent.pointLightInnerRadius <= 0.1f)
            {
                _isGrowing = true;
            }

            lightComponent.pointLightInnerRadius += _isGrowing ? 0.01f : -0.01f;
        }
    }
}