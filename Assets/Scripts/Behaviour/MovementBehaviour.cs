using System;
using Data;
using Data.Enum;
using Feature;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviour
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementFeature movementFeature;
        [SerializeField] private BehaviourModification movementModification;
        [SerializeField] private AbilityFeature abilityFeature;
        [SerializeField] private GameStateFeature gameStateFeature;
        private Rigidbody2D _rigidbody;
        private Transform _transform;

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
            
            if (gameStateFeature == null)
            {
                throw new ArgumentException("No gameStateFeature is defined");
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
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

            Vector3 localScale = _transform.localScale;
            if (localScale.x < 0 && data.movement.x < 0 || localScale.x > 0 && data.movement.x > 0)
            {
                // Flip displayed direction
                localScale.Set(localScale.x * -1, localScale.y, localScale.z);
                _transform.localScale = localScale;
            }
        }

        void OnMove(InputValue value)
        {
            // Ignore keyboard inputs, if the game is paused
            if (gameStateFeature.IsGamePaused())
            {
                movementFeature.SetMovement(Vector2.zero);
                return;
            }
            
            // Listen to player keyboard input
            if (movementFeature.GetMovementData().movementType == MovementType.PlayerInput)
            {
                movementFeature.SetMovement(value.Get<Vector2>());
            }
        }
    }
}