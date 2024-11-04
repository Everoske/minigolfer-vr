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
        private Handedness queuedHand;

        private void Start()
        {
            SwitchActiveReference(Handedness.Left);
            SetActiveHand();
        }

        private void Update()
        {
            MoveMenu();
            HandleShowMenu();
            if (!handMenu.gameObject.activeInHierarchy && activeHand != queuedHand)
            {
                SetActiveHand();
            }
        }
        
        public void SwitchActiveReference(Handedness handedness) 
        {
            queuedHand = handedness;
        }

        private void SetActiveHand()
        {
            activeHand = queuedHand;

            if (activeHand == Handedness.Left)
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