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
        [SerializeField]
        private GolfBall golfBallPrefab;

        /// <summary>
        /// Spawns a golf ball and assigns it a material
        /// </summary>
        /// <param name="assignedMaterial">Material to assign golf ball</param>
        public void SpawnBall(Material assignedMaterial)
        {
            GolfBall ball = Instantiate(golfBallPrefab, RandomSpawnLocation());
            ball.AssignMaterial(assignedMaterial);
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
