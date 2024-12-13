using Minigolf.Putting.Interactable;
using UnityEngine;

namespace Minigolf.XR
{
    /// <summary>
    /// Spawns Mini Golf Balls
    /// </summary>
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] ballSpawns;

        /// <summary>
        /// Spawns a mini golf ball
        /// </summary>
        /// <param name="ballPrefab">Mini golf ball prefab</param>
        public void SpawnBall(GolfBall ballPrefab)
        {
            Instantiate(ballPrefab, RandomSpawnLocation());
        }

        /// <summary>
        /// Returns a random spawn location
        /// </summary>
        /// <returns>Random spawn transform</returns>
        private Transform RandomSpawnLocation()
        {
            int index = Random.Range(0, ballSpawns.Length);
            return ballSpawns[index];
        }
    }
}
