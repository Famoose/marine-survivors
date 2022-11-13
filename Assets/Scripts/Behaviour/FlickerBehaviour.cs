using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Behaviour
{
    public class FlickerBehaviour : MonoBehaviour
    {
        [SerializeField] private Light2D light;
        [SerializeField] private float multiplier = 0.1f;
        private float _interval = 0; 
        private float _lastChange = 0;
        private void Update()
        {
            //change direction if intervall is meet
            if (_lastChange + _interval < Time.time)
            {
                multiplier *= -1;
                _lastChange = Time.time;
                _interval = Random.Range(0.2f, 0.8f);
            }

            light.intensity = Mathf.Clamp(light.intensity + multiplier * Time.deltaTime, 0.1f, 1f);
        }
    }
}