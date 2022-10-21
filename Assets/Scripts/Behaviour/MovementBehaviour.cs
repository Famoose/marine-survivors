using System;
using Data;
using Data.Enum;
using Feature;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Behaviour
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementFeature movementFeature;
        [SerializeField] private BehaviourModification movementModification;
        [SerializeField] private AbilityFeature abilityFeature;
        private Rigidbody2D _rigidbody;

        // modification value
        private float _movementModificationFactor = 1;

        public MovementFeature GetMovementFeature()
        {
            return movementFeature;
        }

        private void Awake()
        {
            if (movementFeature == null)
            {
                throw new ArgumentException("No MovementInputFeature is defined");
            }

            if (abilityFeature == null)
            {
                throw new ArgumentException("No abilityFeature is defined");
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            if (abilityFeature && movementModification)
            {
                CalculateMovementModification();
                //could be improved by check if ability is related to movement
                abilityFeature.onAbilityActivated.AddListener(ad => CalculateMovementModification());
                abilityFeature.onAbilityLevelUp.AddListener(ad => CalculateMovementModification());
            }
        }

        private void CalculateMovementModification()
        {
            float factor = 1;
            foreach (AbilityData abilityData in abilityFeature.GetActiveAbilityByModification(movementModification))
            {
                var valueModifier = abilityData.GetValueModifier();
                //movement only supports factor
                if (valueModifier.type == ValueModifierType.Factor)
                {
                    factor *= valueModifier.value;
                }
            }

            _movementModificationFactor = factor;
        }

        private void FixedUpdate()
        {
            MovementData data = movementFeature.GetMovementData();

            if (data.movementType == MovementType.Constant)
            {
                // Move for a constant value in the current direction
                movementFeature.ApplyConstantMovement();
            }

            _rigidbody.MovePosition(_rigidbody.position +
                                    data.movement * (Time.fixedDeltaTime * _movementModificationFactor));
        }

        void OnMove(InputValue value)
        {
            // Listen to player keyboard input
            if (movementFeature.GetMovementData().movementType == MovementType.PlayerInput)
            {
                movementFeature.SetMovement(value.Get<Vector2>());
            }
        }
    }
}