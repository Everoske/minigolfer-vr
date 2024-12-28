using Minigolf.Putting.Interactable;
using Minigolf.Scriptable;
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
        /// Spawns a Golf Ball
        /// </summary>
        /// <param name="template">Golf ball template for ball</param>
        public void SpawnBall(GolfBallTemplate template)
        {
            GolfBall ball = Instantiate(golfBallPrefab, RandomSpawnLocation());
            ball.Template = template;
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
