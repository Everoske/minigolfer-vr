using Minigolf.Putting.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.Putting.Hole
{
    public class PuttingArea : MonoBehaviour
    {
        [SerializeField]
        private int holePar;

        [SerializeField]
        private PuttingHole hole;

        [SerializeField]
        private StartingArea startingArea;

        public int Par => holePar;

        public PuttingHole Hole => hole;
        public StartingArea StartingArea => startingArea;

        private bool isActive = false;
        private bool hasStarted = false;
        public bool HasStarted => hasStarted;

        public void StartHole()
        {
            if (!isActive) return;

            hasStarted = true;
            hole.SetHoleStatus(true);
        }

        public void ActivatePuttingArea()
        {
            isActive = true;
            hasStarted = false;
        }
    }
}
