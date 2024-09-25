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

        private bool ballHitOnStart = false;

        /*
         * TODO: This will become a private variable later on.
         * The player will select the color of Golf Ball before starting the game.
         * Once they confirm their selection, that ball's prefab will be assigned 
         * here which will be spawned for each putting area
         */
        [SerializeField]
        private GolfBall golfBallPrefab;

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

        private void CompleteHole()
        {
            puttingAreas[currentIndex].Hole.onHoleComplete -= CompleteHole;
            puttingScore.CompleteCurrentCard();
            MoveToNextHole();
        }

        public void StartPuttingGame()
        {
            if (puttingAreas.Length <= 0 || hasStarted) return;

            hasStarted = true;
            currentIndex = 0;
            puttingScore.StartNewPuttingGame();
            endScreenUI.SetActive(false);
            ActivateCurrentHole();
        }

        private void MoveToNextHole()
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
            
        }

        private void EndPuttingGame()
        {
            DespawnBall();
            hasStarted = false;
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
                ballHitOnStart = true;
            }
            else
            {
                puttingScore.IncrementCardHits();
            }
        }

        private void StartCurrentHole()
        {
            if (ballHitOnStart)
            {
                puttingAreas[currentIndex].StartingArea.onLeftStartingArea -= StartCurrentHole;
                ballHitOnStart = false;
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
