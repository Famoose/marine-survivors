using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Behaviour
{
    public class FlickerBehaviour : MonoBehaviour
    {
        [SerializeField] private new Light2D light;
        [SerializeField] private float multiplier = 0.05f;
        [SerializeField] private float baseIntensity = 1f;
        [SerializeField] private float intervalTimeMin = 0.2f;
        [SerializeField] private float intervalTimeMax = 1.4f;
        private void Start()
        {
            StartCoroutine(this.Flicker(light, multiplier, intervalTimeMin, intervalTimeMax, baseIntensity));
        }
        IEnumerator Flicker(Light2D lightToChange, float _multiplier, float _intervalTimeMin, float _intervalTimeMax, float _baseIntensity)
        {
            float _interval = 0;
            float _lastChange = 0;
            while (true)
            {
                if (lightToChange)
                {
                    //change direction if intervall is meet
                    if (_lastChange + _interval < Time.time)
                    {
                        _multiplier *= -1;
                        _lastChange = Time.time;
                        _interval = Random.Range(_intervalTimeMin, _intervalTimeMax);
                    }

                    lightToChange.intensity = Mathf.Clamp(lightToChange.intensity + _multiplier * Time.deltaTime, 0.1f, _baseIntensity);
                }
                yield return new WaitForSeconds( 0.01f);
            }
        }
    }
    
}