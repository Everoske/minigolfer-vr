using Minigolf.Putting.Hole;
using Minigolf.Putting.Interactable;
using UnityEngine;

namespace Minigolf.Putting.Game
{
    public class PuttingGame : MonoBehaviour
    {
        [SerializeField]
        private PuttingArea[] puttingAreas;

        [SerializeField]
        private PuttingScore puttingScore;

        [SerializeField]
        private GameObject endScreenUI;

        private int currentIndex = 0;

        private bool hasStarted = false;

        private bool allowFirstHit = false;

        /*
         * TODO: This will become a private variable later on.
         * The player will select the color of Golf Ball before starting the game.
         * Once they confirm their selection, that ball's prefab will be assigned 
         * here which will be spawned for each putting area
         */
        [SerializeField]
        private GolfBall golfBallPrefab;

        // Only one instance of this should exist at one time
        // Each time a hole is activated, this ball should spawn at the new hole
        private GolfBall playerGolfBall = null;

        private void Start()
        {
            puttingScore.CreateCards(puttingAreas);
        }

        private void OnDisable()
        {
            DespawnBall();
            if (hasStarted && currentIndex < puttingAreas.Length)
            {
                puttingAreas[currentIndex].StartingArea.onLeftStartingArea -= StartCurrentHole;
                puttingAreas[currentIndex].Hole.onHoleComplete -= CompleteHole;
            }
        }

        public void CompleteHole()
        {
            puttingAreas[currentIndex].Hole.onHoleComplete -= CompleteHole;
            puttingScore.CompleteCurrentCard();
            MoveToNextHole();
        }

        public void StartPuttingGame()
        {
            if (puttingAreas.Length <= 0 || hasStarted) return;

            Debug.Log("Starting Putting Game");

            hasStarted = true;
            currentIndex = 0;
            puttingScore.StartNewPuttingGame();
            ActivateCurrentHole();
        }

        public void MoveToNextHole()
        {
            currentIndex++;

            if (currentIndex >= puttingAreas.Length)
            {
                EndPuttingGame();
            }
            else
            {
                ActivateCurrentHole();
                puttingScore.NextCard();
            }
        }

        private void ActivateCurrentHole()
        {
            puttingAreas[currentIndex].ActivatePuttingArea();
            puttingAreas[currentIndex].StartingArea.onLeftStartingArea += StartCurrentHole;
            puttingAreas[currentIndex].Hole.onHoleComplete += CompleteHole;
            SpawnBall(puttingAreas[currentIndex].StartingArea.BallSpawn);

            Debug.Log($"Hole {currentIndex + 1} is active!");
            
        }

        private void EndPuttingGame()
        {
            DespawnBall();
            hasStarted = false;
            Debug.Log("Putting game complete!");
            endScreenUI.SetActive(true);  
        }

        private bool CurrentHoleHasStarted()
        {
            if (currentIndex > puttingAreas.Length) return false;
            return puttingAreas[currentIndex].HasStarted;
        }

        private void HandleBallHit()
        {
            if (!hasStarted) return;

            if (!CurrentHoleHasStarted())
            {
                allowFirstHit = true;
            }
            else
            {
                puttingScore.IncrementCardHits();
            }
        }

        // This can be moved into the Putting Area script
        private void StartCurrentHole()
        {
            if (allowFirstHit)
            {
                puttingAreas[currentIndex].StartingArea.onLeftStartingArea -= StartCurrentHole;
                allowFirstHit = false;
                puttingAreas[currentIndex].StartHole();
                puttingScore.IncrementCardHits();
            }
        }

        private void DespawnBall()
        {
            if (playerGolfBall == null) return;

            playerGolfBall.onBallHit -= HandleBallHit;

            Destroy(playerGolfBall.gameObject);
            playerGolfBall = null;
        }

        public void SpawnBall(Vector3 position)
        {
            if (!hasStarted) return;

            if (playerGolfBall != null)
            {
                DespawnBall();
            }

            playerGolfBall = Instantiate(golfBallPrefab, position, Quaternion.identity);
            playerGolfBall.onBallHit += HandleBallHit;
        }
    }
}
