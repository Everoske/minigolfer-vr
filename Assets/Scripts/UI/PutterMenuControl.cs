using Minigolf.Putting.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    public class PutterMenuControl : MonoBehaviour
    {
        [SerializeField]
        private PuttingGame puttingGame;

        [SerializeField]
        private Button teleportButton;
        [SerializeField]
        private Button toStartButton;
        [SerializeField]
        private Button restartButton;

        private void Start()
        {
            SetNoGame();
        }

        private void OnEnable()
        {
            puttingGame.onStartPuttingGame += SetGameActive;
            puttingGame.onEndPuttingGame += SetNoGame;

            teleportButton.onClick.AddListener(puttingGame.TeleportToCurrentHole);
            toStartButton.onClick.AddListener(puttingGame.TeleportToStart);
            restartButton.onClick.AddListener(puttingGame.RestartPuttingGame);
        }

        private void OnDisable()
        {
            puttingGame.onStartPuttingGame -= SetGameActive;
            puttingGame.onEndPuttingGame -= SetNoGame;

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
        }
    }
}