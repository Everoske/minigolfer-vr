using Minigolf.Putting.Interactable;
using Minigolf.XR;
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

        [SerializeField]
        private TeleportMarker teleportMarker;

        public int Par => holePar;

        public PuttingHole Hole => hole;
        public StartingArea StartingArea => startingArea;

        private bool isActive = false;
        private bool hasStarted = false;
        public bool HasStarted => hasStarted;

        /// <summary>
        /// Starts tracking the number of hits for this hole
        /// </summary>
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

        /// <summary>
        /// Teleport player to the hole's TeleportMarker
        /// </summary>
        public void TeleportToHole()
        {
            teleportMarker.TeleportToMarker();
        }
    }
}
