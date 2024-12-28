using Minigolf.Putting.Game;
using Minigolf.Putting.Interactable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    public class PutterMenuControl : MonoBehaviour
    {
        [SerializeField]
        private PuttingGame puttingGame;

        [SerializeField]
        private PuttingPlayer puttingPlayer;


        [Header("Teleportation and Restart Buttons")]
        [SerializeField]
        private Button teleportButton;
        [SerializeField]
        private Button toStartButton;
        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private TMP_Text currentHoleText;
        [SerializeField]
        private Button spawnBallButton;
        [SerializeField]
        private Image spawnBallImage;

        private void Start()
        {
            SetNoGame();
        }

        private void OnEnable()
        {
            puttingGame.onStartPuttingGame += SetGameActive;
            puttingGame.onEndPuttingGame += SetNoGame;
            puttingGame.onActivateHole += SetCurrentHole;

            puttingPlayer.onBallChanged += ChangeBallIcon;

            teleportButton.onClick.AddListener(puttingGame.TeleportToCurrentHole);
            toStartButton.onClick.AddListener(puttingGame.TeleportToStart);
            restartButton.onClick.AddListener(puttingGame.RestartPuttingGame);
        }

        private void OnDisable()
        {
            puttingGame.onStartPuttingGame -= SetGameActive;
            puttingGame.onEndPuttingGame -= SetNoGame;
            puttingGame.onActivateHole -= SetCurrentHole;

            puttingPlayer.onBallChanged -= ChangeBallIcon;

            teleportButton.onClick.RemoveAllListeners();
            toStartButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Makes it so game control buttons are active
        /// </summary>
        private void SetGameActive()
        {
            toStartButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            teleportButton.gameObject.SetActive(true);
        }

        /// <summary>
        /// Makes it so game control buttons are inactive and teleport to start is active
        /// </summary>
        private void SetNoGame()
        {
            toStartButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);
            teleportButton.gameObject.SetActive(false);

            currentHoleText.text = string.Empty;
        }

        /// <summary>
        /// Sets the Current Hole text field to reflect the currently active hole
        /// </summary>
        /// <param name="holeNumber"></param>
        private void SetCurrentHole(int holeNumber)
        {
            currentHoleText.text = $"Current Hole: {holeNumber}";
        }

        /// <summary>
        /// Sets the Spawn Ball Button image to match the player's current ball
        /// </summary>
        /// <param name="ball">Player's selected golf ball</param>
        /// <param name="active">Determines whether the spawn ball button is interactable</param>
        private void ChangeBallIcon(GolfBall ball, bool active)
        {
            spawnBallImage.sprite = ball.UIIcon;
            spawnBallButton.interactable = active;
        }
    }
}