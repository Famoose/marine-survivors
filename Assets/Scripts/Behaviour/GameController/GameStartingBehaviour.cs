using System;
using Feature;
using UnityEngine;

namespace Behaviour
{
    [RequireComponent(typeof(GameStateFeature))]
    public class GameStartingBehaviour : MonoBehaviour
    {
        [SerializeField] private GameStateFeature gameStateFeature;

        private void Start()
        {
            // Unpause the game on start. This is necessary due to the persisting of game state data.
            gameStateFeature.UnpauseGame();
        }
    }
}