using Data;
using Feature;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviour
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField]
        private MovementInputFeature movementInputFeature;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            MovementData data = movementInputFeature.GetMovementData();
            Debug.Log("${data.playerMovement.x}, ${data.playerMovement.y}");
            _rb.MovePosition(_rb.position + data.playerMovement * (data.playerSpeed * Time.fixedDeltaTime));
        }
        
        void OnMove(InputValue value)
        {
            Debug.Log("move");
            movementInputFeature.SetPlayerMovement(value.Get<Vector2>());
        }

    }
}
