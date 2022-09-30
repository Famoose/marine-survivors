using System;
using Data;
using Data.Enum;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Feature
{
    public class MovementFeature : MonoBehaviour
    {
        [SerializeField] private MovementData initialData;
        private MovementData _data;
        private void Awake()
        {
            if (initialData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<MovementData>();
            _data.movementType = initialData.movementType;
            _data.initialMovementType = initialData.initialMovementType;
            _data.speed = initialData.speed;
            _data.movement = initialData.movement;
        }

        private void Start()
        {
            switch (_data.initialMovementType)
            {
                case InitialMovementType.DerivedFromMovement:
                    SetMovement(_data.movement);
                    break;
                case InitialMovementType.Random:
                    SetMovement(new Vector2(
                        Random.Range(-1f, 1f), 
                        Random.Range(-1f, 1f))
                        .normalized);
                    break;
            }
        }

        public MovementData GetMovementData()
        {
            return _data;
        }
        public void SetMovement(Vector2 value)
        {
            _data.movement = value.normalized * _data.speed;
        }

        public void ApplyConstantMovement()
        {
            _data.movement = _data.movement.normalized * _data.speed;
        }
    }
}