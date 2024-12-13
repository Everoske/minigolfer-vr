using Minigolf.Putting.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.XR
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnLocation;

        public void SpawnBall(GolfBall ballPrefab)
        {
            Instantiate(ballPrefab, spawnLocation);
        }
    }
}
