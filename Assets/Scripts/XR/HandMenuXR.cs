using Minigolf.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Minigolf.XR
{
    public class HandMenuXR : MonoBehaviour
    {
        [SerializeField]
        private Camera playerCamera;

        [SerializeField]
        private GameObject leftHandRef;

        [SerializeField]
        private GameObject rightHandRef;

        [SerializeField]
        private float visibleDistanceLimit = 0.25f;

        [SerializeField]
        private HandMenuController handMenu;

        [SerializeField]
        private LayerMask menuMask;

        [SerializeField]
        private GameObject activeReference;

        private GameObject activeHandRef;

        // TODO: PAY ATTENTION
        // Implement:
        // - Check to see if player is hovering, holding, or otherwise using the menu hand
        // - Hand Menu should only open if player is not using the activeReference hand
        // Note:
        // - Handedness might need to be tracked else where
        // - Preferred handedness should be saved in a settings file and loaded from that file on start


        private void Start()
        {
            SwitchActiveReference(Handedness.Left);
        }

        private void Update()
        {
            MoveMenu();
            HandleShowMenu();
        }
        

        // TODO: Consider adding a queue so the swap happens only after the player closes the menu
        public void SwitchActiveReference(Handedness handedness) 
        {
            if (handedness == Handedness.Left)
            {
                rightHandRef.SetActive(false);
                leftHandRef.SetActive(true);
                activeHandRef = leftHandRef;
            }
            else
            {
                rightHandRef.SetActive(true);
                leftHandRef.SetActive(false);

                activeHandRef = rightHandRef;
            }
        }


        private void HandleShowMenu()
        {
            if (!LookingAtRef())
            {
                handMenu.CloseHandMenu();
            }
            else
            {
                handMenu.OpenHandMenu();
            }
        }

        private void MoveMenu()
        {
            if (activeHandRef == null) return;

            handMenu.transform.position = activeHandRef.transform.position;
            handMenu.transform.rotation = activeHandRef.transform.rotation;
        }

        private bool LookingAtRef()
        {
            return Physics.Raycast(
                playerCamera.transform.position, 
                playerCamera.transform.forward, 
                visibleDistanceLimit,
                menuMask
                );
        }
    }

    public enum Handedness
    {
        Left,
        Right
    }
}