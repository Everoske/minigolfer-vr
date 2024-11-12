using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Minigolf.XR
{
    public class TeleportMarker : MonoBehaviour
    {
        [SerializeField]
        private TeleportationProvider teleportProvider;

        [SerializeField]
        private Transform destination;

        [SerializeField]
        private MatchOrientation matchOrientation;
       
        /// <summary>
        /// Generate a Teleportation Request to the destination position
        /// </summary>
        public void TeleportToMarker()
        {
            TeleportRequest request = new TeleportRequest();

            request.matchOrientation = matchOrientation;
            request.destinationPosition = destination.position;
            request.destinationRotation = destination.rotation;

            teleportProvider.QueueTeleportRequest(request);
        }
    }
}
