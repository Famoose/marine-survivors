using System;
using Data.Enum;
using Feature;
using UnityEngine;

namespace Behaviour
{
    public class InflictDamageOnCollisionBehaviour : MonoBehaviour
    {
        private readonly string[] _validTags = {"Enemy", "Player Projectile"};
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

            if (!ComponentHasValidTag())
            {
                //throw new ArgumentException("The projectile does not contain the 'Player Projectile'-tag");
                throw new ArgumentException("The inflicter behaviour component does not contain a valid tag. (Player Projectile or Enemy)");
            }
        }

        private bool ComponentHasValidTag()
        {
            int found = Array.FindIndex(_validTags, el => el.Equals(tag));
            return found > -1;
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

            if (inflictDamageOnCollisionFeature.GetTypeWhichIsIgnoredForCollision() == ActiveGameObjectType.Enemy &&
                collision.collider.gameObject.CompareTag("Enemy"))
            {
                // Ignore collisions with other enemies
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