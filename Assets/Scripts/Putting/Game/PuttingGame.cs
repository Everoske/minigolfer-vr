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
        private PuttingPlayer puttingPlayer;

        [SerializeField]
        private PuttingScore puttingScore;

        [SerializeField]
        private GameObject endScreenUI;

        [SerializeField]
        private TeleportPlayer endGameTeleport;

        private int currentIndex = 0;

        private bool hasStarted = false;

        private bool allowFirstHit = false;


        private void Start()
        {
            puttingScore.CreateCards(puttingAreas);
        }

        private void OnDisable()
        {
            puttingPlayer.PlayerGolfBall.onBallHit -= HandleBallHit;
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
            if (puttingAreas.Length <= 0) return;

            Debug.Log("Starting Putting Game");

            puttingPlayer.PlayerGolfBall.onBallHit += HandleBallHit;
            
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
            Debug.Log($"Hole {currentIndex + 1} is active!");
            
        }

        private void EndPuttingGame()
        {
            puttingPlayer.PlayerGolfBall.onBallHit -= HandleBallHit;
            hasStarted = false;
            Debug.Log("Putting game complete!");
            endScreenUI.SetActive(true);
            endGameTeleport.Teleport();    
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
