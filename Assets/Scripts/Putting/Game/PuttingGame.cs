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
        private GolfBall playerGolfBall;

        private void Start()
        {
            puttingScore.CreateCards(puttingAreas);
        }

        private void OnDisable()
        {
            playerGolfBall.onBallHit -= HandleBallHit;
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

            playerGolfBall.onBallHit += HandleBallHit;
            
            currentIndex = 0;
            puttingScore.StartNewPuttingGame();
            ActivateCurrentHole();
            hasStarted = true;
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
            // Teleport ball
            Debug.Log($"Hole {currentIndex + 1} is active!");
            
        }

        private void EndPuttingGame()
        {
            //puttingPlayer.PlayerGolfBall.onBallHit -= HandleBallHit;
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
    }
}
