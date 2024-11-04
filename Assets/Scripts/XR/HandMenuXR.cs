using Minigolf.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

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
        private XRDirectInteractor leftDirectInteractor;
        [SerializeField]
        private XRDirectInteractor rightDirectInteractor;

        [SerializeField]
        private float visibleDistanceLimit = 0.25f;

        [SerializeField]
        private HandMenuController handMenu;

        [SerializeField]
        private LayerMask menuMask;

        private GameObject activeReference;
        private GameObject activeHandRef;
        private Handedness activeHand;

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
                
                leftHandRef.SetActive(false);
                rightHandRef.SetActive(true);

                activeHandRef = rightHandRef;
            }

            activeHand = handedness;
        }


        private void HandleShowMenu()
        {
            if (LookingAtRef() && CanOpenMenu())
            {
                handMenu.OpenHandMenu();
            }
            else
            {
                handMenu.CloseHandMenu();
            }
            
        }

        private bool CanOpenMenu()
        {
            if (activeHand == Handedness.Left)
            {
                return !leftDirectInteractor.hasHover && !leftDirectInteractor.hasSelection;
            }

            return !rightDirectInteractor.hasHover && !rightDirectInteractor.hasSelection;
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