using System;
using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Feature
{
    public class MovementInputFeature : MonoBehaviour
    {
        [SerializeField] private MovementData initialData;
        private MovementData _data;
        public void Awake()
        {
            if (initialData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<MovementData>();
            _data.playerSpeed = initialData.playerSpeed;
        }

        public MovementData GetMovementData()
        {
            return _data;
        }
        public void SetPlayerMovement(Vector2 value)
        {
            _data.playerMovement = value;
        }
    }
}