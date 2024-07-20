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

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GolfBall>(out GolfBall ball))
            {
                onLeftStartingArea?.Invoke();
                Debug.Log("Ball Left Starting Area!");
            }
        }
    }
}
