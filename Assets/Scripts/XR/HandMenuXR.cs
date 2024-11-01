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
        private Transform leftControllerTransform;

        [SerializeField]
        private float visibleDistanceLimit = 0.45f;

        [SerializeField]
        private HandMenuController handMenu;

        [SerializeField]
        private LayerMask menuMask;

        private GameObject activeHandRef;

        private void Update()
        {
            MoveMenu();
            HandleShowMenu();
        }
        

        // Switch Hand Reference from Left to Right
        private void SwitchActiveReference()
        {

        }

        private void DeactiveReference(GameObject reference)
        {
            reference.SetActive(false);
            reference.GetComponent<SphereCollider>().enabled = false;
        }

        private void ActivateReference(GameObject reference)
        {
            reference.SetActive(true);
            reference.GetComponent<SphereCollider>().enabled = true;
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
            handMenu.transform.position = leftHandRef.transform.position;
            handMenu.transform.rotation = leftHandRef.transform.rotation;
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
}