using Data;
using Data.Enum;
using Feature;
using UnityEngine;

namespace Behaviour.item
{
    public class MagnetBehaviour : MonoBehaviour, IItemBehaviour
    {
        public void ActivateItem(GameObject activator)
        {
            GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
            MovementData movementData = ScriptableObject.CreateInstance<MovementData>();
            movementData.speed = 20;
            movementData.movementType = MovementType.FollowTarget;
            foreach (var collectable in collectables)
            {
                TrackingFeature ptf = collectable.AddComponent<TrackingFeature>();
                ptf.SetTarget(activator);
                MovementFeature movementFeature = collectable.AddComponent<MovementFeature>();
                movementFeature.Initialize(movementData);
                TargetMovementBehaviour targetMovement = collectable.AddComponent<TargetMovementBehaviour>();
                targetMovement.trackingFeature = ptf;
                targetMovement.movementFeature = movementFeature;
            }
        }
    }
}