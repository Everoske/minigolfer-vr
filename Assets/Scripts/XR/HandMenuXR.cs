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

        // TESTING ONLY: REMOVE
        [SerializeField]
        private GameObject handMenu;

        [SerializeField]
        private LayerMask menuMask;


        private void Update()
        {
            MoveMenu();
            HandleShowMenu();
        }

        // Make Hand Menu Active and Fade In Using LERP
        private void FadeIn()
        {

        }

        // Fade Out Using LERP then Deactivate Hand Menu
        private void FadeOut()
        {

        }


        private void HandleShowMenu()
        {
            if (!LookingAtRef())
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            handMenu.SetActive(true);
        }

        private void HideMenu()
        {
            handMenu.SetActive(false);
        }

        private void MoveMenu()
        {
            handMenu.transform.position = leftHandRef.transform.position;
            handMenu.transform.rotation = leftControllerTransform.rotation;
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

        private void OnDrawGizmos()
        {
            Debug.DrawRay(leftHandRef.transform.position, leftHandRef.transform.right, Color.blue);
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);

            Vector3 direct = leftHandRef.transform.position - playerCamera.transform.position;
            Debug.DrawRay(playerCamera.transform.position, direct, Color.magenta);
        }
    }

}