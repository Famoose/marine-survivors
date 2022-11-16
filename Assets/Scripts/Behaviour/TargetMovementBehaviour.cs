using Data;
using Data.Enum;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class TargetMovementBehaviour : MonoBehaviour
    { 
        public MovementFeature movementFeature;
        public TrackingFeature trackingFeature;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MovementData data = movementFeature.GetMovementData();
            GameObject player = trackingFeature.GetTarget();
            if (player)
            {
                Vector3 direction = Vector3.zero;
                Vector3 position = transform.position;
                
                if (data.movementType == MovementType.Constant)
                {
                    if (data.movement == Vector2.zero)
                    {
                        direction = (player.transform.position - position).normalized;
                        movementFeature.SetMovement(direction);
                    }
                    else
                    {
                        direction = data.movement;
                    }
                }

                if (data.movementType == MovementType.FollowTarget)
                {
                     direction = (player.transform.position - position).normalized;
                }
                
                _rigidbody.MovePosition(position + direction * (Time.fixedDeltaTime * data.speed));

            }
        }
    }
}