using System;
using Data.Enum;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class InflictDamageOnCollisionBehaviour : MonoBehaviour
    {
        [SerializeField] private InflictDamageOnCollisionFeature inflictDamageOnCollisionFeature;
        private Collider2D _collider;

        public InflictDamageOnCollisionFeature GetInflictDamageOnCollisionFeature()
        {
            return inflictDamageOnCollisionFeature;
        }

        private void Awake()
        {
            if (inflictDamageOnCollisionFeature == null)
            {
                throw new ArgumentException("No InflictDamageOnCollisionFeature is defined");
            }

            _collider = GetComponent<Collider2D>();
            if (_collider == null)
            {
                throw new ArgumentException("No Collider is defined");
            }

            if (!CompareTag("Player Projectile"))
            {
                throw new ArgumentException("The projectile does not contain the 'Player Projectile'-tag");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.CompareTag("Player Projectile"))
            {
                // Projectiles do not collide with each other
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
                return;
            }

            if (inflictDamageOnCollisionFeature.GetTypeWhichIsIgnoredForCollision() == ActiveGameObjectType.Player &&
                collision.collider.gameObject.CompareTag("Player"))
            {
                // Ignore collisions with the player object (no suicides here...)
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
                return;
            }
            
            DamageableBehaviour othersDamageableBehaviour =
                collision.collider.gameObject.GetComponent<DamageableBehaviour>();
            if (othersDamageableBehaviour != null)
            {
                othersDamageableBehaviour.InflictDamage(inflictDamageOnCollisionFeature.GetDamageToInflict());
            }
            
            if (inflictDamageOnCollisionFeature.ShallDestroyAfterInflictingDamage())
            {
                Destroy(gameObject);
            }
        }
    }
}