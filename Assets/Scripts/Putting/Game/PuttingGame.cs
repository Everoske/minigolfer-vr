using Minigolf.Putting.Hole;
using Minigolf.Putting.Interactable;
using Minigolf.XR;
using UnityEngine;
using UnityEngine.Events;

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

        [SerializeField]
        private GameObject startButton;

        [SerializeField]
        private GameObject restartButton;

        [SerializeField]
        private TeleportMarker startTeleportMarker;

        public UnityAction onStartPuttingGame;
        public UnityAction onEndPuttingGame;
        public UnityAction<int> onActivateHole;

        private int currentIndex = 0;

        private bool gameActive = false;

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
            if (gameActive && currentIndex < puttingAreas.Length)
            {
                puttingAreas[currentIndex].StartingArea.onLeftStartingArea -= StartCurrentHole;
                puttingAreas[currentIndex].Hole.onHoleComplete -= CompleteHole;
            }
        }

        /// <summary>
        /// Finalizes the current hole and marks it as complete
        /// </summary>
        private void CompleteHole()
        {
            puttingAreas[currentIndex].Hole.onHoleComplete -= CompleteHole;
            puttingScore.CompleteCurrentCard();
            MoveToNextHole();
        }

        /// <summary>
        /// Initializes a putting game
        /// </summary>
        public void StartPuttingGame()
        {
            if (puttingAreas.Length <= 0 || gameActive) return;

            if (golfBallPrefab == null)
            {
                // Display message to player that they cannot start the 
                // putting game without a ball
                Debug.Log("Cannot start putting game without ball");
                return;
            }

            gameActive = true;
            currentIndex = 0;
            puttingScore.StartNewPuttingGame();

            endScreenUI.SetActive(false);
            startButton.SetActive(false);
            restartButton.SetActive(true);

            onStartPuttingGame?.Invoke();

            ActivateCurrentHole();
        }

        /// <summary>
        /// Closes the current game and restarts the player at hole 1
        /// </summary>
        public void RestartPuttingGame()
        {
            if (!gameActive) return;

            DeactivateCurrentHole();
            DespawnBall();

            gameActive = false;
            StartPuttingGame();
        }

        /// <summary>
        /// Moves to the next hole in the game
        /// </summary>
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

        /// <summary>
        /// Adds listeners to the current hole and starts awaiting input from player
        /// </summary>
        private void ActivateCurrentHole()
        {
            onActivateHole?.Invoke(currentIndex + 1);
            puttingAreas[currentIndex].ActivatePuttingArea();
            puttingAreas[currentIndex].StartingArea.onLeftStartingArea += StartCurrentHole;
            puttingAreas[currentIndex].Hole.onHoleComplete += CompleteHole;
            SpawnBall(puttingAreas[currentIndex].StartingArea.BallSpawn);
        }

        /// <summary>
        /// Removes listeners from the current hole
        /// </summary>
        private void DeactivateCurrentHole()
        {
            if (!CurrentHoleHasStarted())
            {
                puttingAreas[currentIndex].StartingArea.onLeftStartingArea -= StartCurrentHole;
            }

            puttingAreas[currentIndex].Hole.onHoleComplete -= CompleteHole;
        }

        /// <summary>
        /// Ends the current putting game
        /// </summary>
        private void EndPuttingGame()
        {
            DespawnBall();
            gameActive = false;

            endScreenUI.SetActive(true);
            startButton.SetActive(true);
            restartButton.SetActive(false);

            onEndPuttingGame?.Invoke();
        }

        /// <summary>
        /// Determines if the current hole has been started by the player 
        /// </summary>
        /// <returns></returns>
        private bool CurrentHoleHasStarted()
        {
            if (currentIndex > puttingAreas.Length) return false;
            return puttingAreas[currentIndex].HasStarted;
        }

        /// <summary>
        /// Determines how to process the hitting of the player's golf ball
        /// </summary>
        private void HandleBallHit()
        {
            if (!gameActive) return;

            if (!CurrentHoleHasStarted())
            {
                ballHitOnStart = true;
            }
            else
            {
                puttingScore.IncrementCardHits();
            }
        }

        /// <summary>
        /// Starts the current hole
        /// </summary>
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

        /// <summary>
        /// Destroys the active instance of the player's ball
        /// </summary>
        private void DespawnBall()
        {
            if (playerGolfBall == null) return;

            playerGolfBall.onBallHit -= HandleBallHit;

            Destroy(playerGolfBall.gameObject);
            playerGolfBall = null;
        }

        /// <summary>
        /// Spawns the player's ball at the given world position
        /// </summary>
        /// <param name="position">Spawn Location</param>
        public void SpawnBall(Vector3 position)
        {
            if (!gameActive) return;

            if (playerGolfBall != null)
            {
                DespawnBall();
            }

            playerGolfBall = Instantiate(golfBallPrefab, position, Quaternion.identity);
            playerGolfBall.onBallHit += HandleBallHit;
        }

        /// <summary>
        /// Teleports Player to Active Hole
        /// </summary>
        public void TeleportToCurrentHole()
        {
            if (!gameActive) return;

            puttingAreas[currentIndex].TeleportToHole();
        }

        /// <summary>
        /// Teleport the player to the starting area
        /// </summary>
        public void TeleportToStart()
        {
            startTeleportMarker.TeleportToMarker();
        }
    }
}
