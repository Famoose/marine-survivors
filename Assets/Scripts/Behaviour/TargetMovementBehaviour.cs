using System.Collections;
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
            GameObject target = trackingFeature.GetTarget();
            StartCoroutine(MoveToTarget(_rigidbody, target, movementFeature));
        }

        IEnumerator MoveToTarget(Rigidbody2D rb, GameObject target, MovementFeature mf)
        {
            MovementData data = movementFeature.GetMovementData();

            while (true)
            {
                if (target)
                {
                    Vector3 direction = Vector3.zero;
                    Vector3 position = transform.position;
                
                    if (data.movementType == MovementType.Constant)
                    {
                        if (data.movement == Vector2.zero)
                        {
                            direction = (target.transform.position - position).normalized;
                            mf.SetMovement(direction);
                        }
                        else
                        {
                            direction = data.movement;
                        }
                    }

                    if (data.movementType == MovementType.FollowTarget)
                    {
                        direction = (target.transform.position - position).normalized;
                    }
                
                    rb.MovePosition(position + direction * (Time.fixedDeltaTime * data.speed));

                }
                yield return new WaitForSeconds( 0.01f);
            }
        }
    }
}