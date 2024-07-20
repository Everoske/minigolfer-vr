using Minigolf.Putting.Interactable;
using UnityEngine;
using UnityEngine.Events;

namespace Minigolf.Putting.Hole
{
    public class PuttingHole : MonoBehaviour
    {
        private bool holeIsActive = false;

        public UnityAction onHoleComplete;

        public void SetHoleStatus(bool active) => holeIsActive = active;

        private void OnTriggerEnter(Collider other)
        {
            if (holeIsActive && other.TryGetComponent<GolfBall>(out GolfBall golfBall))
            {
                holeIsActive = false;
                onHoleComplete?.Invoke();
            }
        }
    }
}
