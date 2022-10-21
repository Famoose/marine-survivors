using Data;
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
            foreach (var collectable in collectables)
            {
                PlayerTrackingFeature ptf = collectable.AddComponent<PlayerTrackingFeature>();
                ptf.SetPlayer(activator);
                MovementFeature movementFeature = collectable.AddComponent<MovementFeature>();
                movementFeature.Initialize(movementData);
                AIMovementBehaviour aiMovement = collectable.AddComponent<AIMovementBehaviour>();
                aiMovement.playerTrackingFeature = ptf;
                aiMovement.movementFeature = movementFeature;
            }
        }
    }
}