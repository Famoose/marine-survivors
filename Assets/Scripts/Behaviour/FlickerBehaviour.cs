using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Behaviour
{
    public class FlickerBehaviour : MonoBehaviour
    {
        [SerializeField] private Light2D light;
        [SerializeField] private float multiplier = 0.05f;
        [SerializeField] private float baseIntensity = 1f;
        [SerializeField] private float intervalTimeMin = 0.2f;
        [SerializeField] private float intervalTimeMax = 1.4f;
        private float _interval = 0; 
        private float _lastChange = 0;
        private void Update()
        {
            //change direction if intervall is meet
            if (_lastChange + _interval < Time.time)
            {
                multiplier *= -1;
                _lastChange = Time.time;
                _interval = Random.Range(intervalTimeMin, intervalTimeMax);
            }

            light.intensity = Mathf.Clamp(light.intensity + multiplier * Time.deltaTime, 0.1f, baseIntensity);
        }
    }
}