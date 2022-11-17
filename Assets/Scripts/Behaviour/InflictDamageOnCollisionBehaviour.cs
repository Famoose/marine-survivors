using System;
using System.Collections.Generic;
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
                throw new ArgumentException(
                    "The inflicter behaviour component does not contain a valid tag. (Player Projectile or Enemy)");
            }
        }

        private bool ComponentHasValidTag()
        {
            int found = Array.FindIndex(_validTags, el => el.Equals(tag));
            return found > -1;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (ShouldIgnoreCollision(collision.collider, collision.otherCollider))
            {
                return;
            }

            var hasRadius = inflictDamageOnCollisionFeature.GetRadius();
            if (hasRadius.HasValue)
            {
                CreateRaidusTriggerDamage(hasRadius.Value);
            }
            else
            {
                InflictDamageOnColliderGameObject(collision.collider);
            }


            if (inflictDamageOnCollisionFeature.ShallDestroyAfterInflictingDamage())
            {
                Destroy(gameObject);
            }
        }

        bool ShouldIgnoreCollision(Collider2D collider, Collider2D otherCollider)
        {
            if (collider.gameObject.CompareTag("Player Projectile"))
            {
                // Projectiles do not collide with each other
                Physics2D.IgnoreCollision(collider, otherCollider);
                return true;
            }

            if (inflictDamageOnCollisionFeature.GetTypeWhichIsIgnoredForCollision() == ActiveGameObjectType.Player &&
                collider.gameObject.CompareTag("Player"))
            {
                // Ignore collisions with the player object (no suicides here...)
                Physics2D.IgnoreCollision(collider, otherCollider);
                return true;
            }

            if (inflictDamageOnCollisionFeature.GetTypeWhichIsIgnoredForCollision() == ActiveGameObjectType.Enemy &&
                collider.gameObject.CompareTag("Enemy"))
            {
                // Ignore collisions with other enemies
                return true;
            }

            return false;
        }

        void CreateRaidusTriggerDamage(float radius)
        {
            var tempTriggerCollider = gameObject.AddComponent<CircleCollider2D>();
            tempTriggerCollider.isTrigger = true;
            tempTriggerCollider.radius = radius;
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            tempTriggerCollider.OverlapCollider(filter, results);
            results.ForEach(r =>
            {
                if(ShouldIgnoreCollision(r, tempTriggerCollider))
                {
                    return;
                }
                InflictDamageOnColliderGameObject(r);
            });
        }

        void InflictDamageOnColliderGameObject(Collider2D collider)
        {
            DamageableBehaviour othersDamageableBehaviour =
                collider.gameObject.GetComponent<DamageableBehaviour>();
            if (othersDamageableBehaviour != null)
            {
                othersDamageableBehaviour.InflictDamage(inflictDamageOnCollisionFeature.GetDamageToInflict());
            }
        }
    }
}