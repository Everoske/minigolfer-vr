using Minigolf.Putting.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigolf.Putting.Hole
{
    public class StartingArea : MonoBehaviour
    {
        public UnityAction onLeftStartingArea;

        public Vector3 BallSpawn
        {
            get => ballSpawn;
        }
        private Vector3 ballSpawn;

        private void Awake()
        {
            Transform spawnTransform = GetComponentInChildren<Transform>();
            if (ballSpawn != null )
            {
                ballSpawn = spawnTransform.position;
            }
            else
            {
                ballSpawn = transform.position;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GolfBall>(out GolfBall ball))
            {
                onLeftStartingArea?.Invoke();
            }
        }
    }
}
