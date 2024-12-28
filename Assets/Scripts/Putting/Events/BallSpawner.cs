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
        /// Spawns a golf ball
        /// </summary>
        /// <param name="ball">Golf Ball Prefab to Spawn</param>
        public void SpawnBall(GolfBall ball)
        {
            Instantiate(ball, RandomSpawnLocation());
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
