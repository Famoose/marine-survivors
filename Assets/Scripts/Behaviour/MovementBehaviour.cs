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

        private void Awake()
        {
            if (movementFeature == null)
            {
                throw new ArgumentException("No MovementInputFeature is defined");
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MovementData data = movementFeature.GetMovementData();

            switch (data.movementType)
            {
                case MovementType.Constant:
                    // Move for a constant value in the current direction
                    movementFeature.ApplyConstantMovement();
                    break;
                case MovementType.ToPlayersDirection:
                    // TODO: Move towards the player
                    throw new NotImplementedException();
            }

            _rigidbody.MovePosition(_rigidbody.position + data.movement * Time.fixedDeltaTime);
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