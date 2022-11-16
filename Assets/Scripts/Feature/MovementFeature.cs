using System;
using Data;
using Data.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Feature
{
    public class MovementFeature : MonoBehaviour
    {
        [SerializeField] private MovementData initialData;
        private MovementData _data;
        private Vector2 _parentDirection;
        public bool IsInitialized { get; private set; }

        public void Initialize(MovementData movementData)
        {
            Initialize(movementData, Vector2.zero);
        }

        public void Initialize(MovementData movementData, Vector2 parentDirection)
        {
            if (movementData == null)
            {
                throw new ArgumentException("initialData was null");
            }
            _data = ScriptableObject.CreateInstance<MovementData>();
            _data.movementType = movementData.movementType;
            _data.initialMovementType = movementData.initialMovementType;
            _data.speed = movementData.speed;
            _data.movement = movementData.movement;

            _parentDirection = parentDirection;

            IsInitialized = true;
            StartMovement();
        }
        
        private void Awake()
        {
            if (initialData != null)
            {
                Initialize(initialData);
            }
        }

        private void StartMovement()
        {
            switch (_data.initialMovementType)
            {
                case InitialMovementType.DerivedFromMovement:
                    SetMovement(_data.movement);
                    break;
                case InitialMovementType.DerivedFromParentHorizontal:
                    SetMovement(new Vector2(-1 * _parentDirection.normalized.x, 0f));
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
            if (!IsInitialized)
            {
                return;
            }
            _data.movement = value.normalized * _data.speed;
        }

        public void ApplyConstantMovement()
        {
            if (!IsInitialized)
            {
                return;
            }
            _data.movement = _data.movement.normalized * _data.speed;
        }

        public void ApplyMovementAndReduceSpeed()
        {
            if (!IsInitialized)
            {
                return;
            }
            ApplyConstantMovement();
            _data.speed = Math.Max(_data.speed - 0.01f, 0f);
        }
    }
}