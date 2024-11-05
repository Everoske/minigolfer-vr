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
        private SphereCollider handRefPrefab;
        
        [Header("Main Interaction Objects")]
        [SerializeField]
        private Camera playerCamera;
        [SerializeField]
        private GameObject leftHandParent;
        [SerializeField] 
        private GameObject rightHandParent;
        [SerializeField]
        private XRDirectInteractor leftDirectInteractor;
        [SerializeField]
        private XRDirectInteractor rightDirectInteractor;
        [SerializeField]
        private HandMenuController handMenu;

        [Header("Menu Offset")]
        [SerializeField]
        private float offsetX = 0.18f;
        [SerializeField]
        private float offsetY = 0.02f;
        [SerializeField]
        private float offsetZ = 0.0f;

        [Header("Menu Activation Parameters")]
        [SerializeField]
        private float detectionRadius = 0.25f;
        [SerializeField]
        private float visibleDistanceLimit = 0.25f;
        [SerializeField]
        private LayerMask menuMask;

        private GameObject leftHandRef;
        private GameObject rightHandRef;

        private GameObject activeReference;
        private GameObject activeHandRef;
        private Handedness activeHand;
        private Handedness queuedHand;

        private void Start()
        {
            CreateHandReferences();
            SwitchActiveReference(Handedness.Left);
            SetActiveHand();
        }

        private void Update()
        {
            MoveMenu();
            HandleShowMenu();
            HandleChangeHands();
        }
        
        private void CreateHandReferences()
        {
            leftHandRef = Instantiate(handRefPrefab.gameObject, leftHandParent.transform);
            leftHandRef.transform.SetLocalPositionAndRotation(leftHandRef.transform.localPosition + new Vector3(offsetX, offsetY, offsetZ), Quaternion.identity);
            leftHandRef.GetComponent<SphereCollider>().radius = detectionRadius;

            rightHandRef = Instantiate(handRefPrefab.gameObject, rightHandParent.transform);
            rightHandRef.transform.SetLocalPositionAndRotation(rightHandRef.transform.localPosition + new Vector3(-offsetX, offsetY, offsetZ), Quaternion.identity);
            rightHandRef.GetComponent<SphereCollider>().radius = detectionRadius;
        }

        public void SwitchActiveReference(Handedness handedness) 
        {
            queuedHand = handedness;
        }

        private void HandleChangeHands()
        {
            if (activeHand == queuedHand) return;
            if (handMenu.gameObject.activeInHierarchy) return;

            SetActiveHand();
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