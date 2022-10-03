using System;
using Data;
using Data.Enum;
using Feature;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Behaviour
{
    public class AIMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementFeature movementFeature;
        [SerializeField] private PlayerTrackingFeature playerTrackingFeature;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            if (movementFeature == null)
            {
                throw new ArgumentException("No movementInputFeature is defined");
            }
            if (playerTrackingFeature == null)
            {
                throw new ArgumentException("No playerTrackingFeature is defined");
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MovementData data = movementFeature.GetMovementData();
            GameObject player = playerTrackingFeature.GetPlayer();
            if (player)
            {
                var position = transform.position;
                var direction = player.transform.position - position;
                _rigidbody.MovePosition(position + direction.normalized * (Time.fixedDeltaTime * data.speed));

            }
        }
    }
}