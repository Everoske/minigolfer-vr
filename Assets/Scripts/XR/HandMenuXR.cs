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
        private float fovHalfHeight = 0.25f;

        [SerializeField]
        private float lookingAtAngleLimit = 45.0f;

        // TESTING ONLY: REMOVE
        [SerializeField]
        private TextMeshProUGUI testDistanceTextL;

        // TESTING ONLY: REMOVE
        [SerializeField]
        private GameObject handMenu;

        private void Update()
        {
            MoveMenu();
            HandleShowMenu();
        }


        private void HandleShowMenu()
        {
            float distance3D = Vector3.Distance(leftHandRef.transform.position, playerCamera.transform.position);

            if (distance3D > visibleDistanceLimit || !LookingAtRef())
            {
                HideMenu();
                return;
            }

            ShowMenu();
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
            Vector2 xyCamForward = new Vector2(playerCamera.transform.forward.x, playerCamera.transform.forward.y);
            Vector2 xyCamPosition = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.y);
            Vector2 xyRefPosition = new Vector2(leftHandRef.transform.position.x, leftHandRef.transform.position.y);

            if (!LookingAtRef2(xyCamForward, xyCamPosition, xyRefPosition)) return false;

            Vector2 xzCamForward = new Vector2(playerCamera.transform.forward.x, playerCamera.transform.forward.z);
            Vector2 xzCamPosition = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.z);
            Vector2 xzRefPosition = new Vector2(leftHandRef.transform.position.x, leftHandRef.transform.position.z);

            return LookingAtRef2(xzCamForward, xzCamPosition, xzRefPosition);
        }

        private bool LookingAtRef(Vector2 camForward, Vector2 camPosition, Vector2 refPosition)
        {
            Vector2 camRef = (refPosition - camPosition).normalized;
            float theta = Vector2.SignedAngle(camForward.normalized, camRef);

            if (theta < 0) Debug.Log($"Theta: {theta}");
      
            return theta <= lookingAtAngleLimit;
        }

        private bool LookingAtRef2(Vector2 camForward, Vector2 camPosition, Vector2 refPosition)
        {
            float upperY = camPosition.y + fovHalfHeight;
            float lowerY = camPosition.y - fovHalfHeight;

            if (refPosition.y <= upperY || refPosition.y >= lowerY) return true;

            //Vector2 upperCam = new Vector2(camPosition.x, camPosition.y + fovHalfHeight);
            //Vector2 lowerCam = new Vector2(camPosition.x, camPosition.y - fovHalfHeight);

            //Vector2 upperCamRef = (refPosition - upperCam).normalized; 
            //Vector2 lowerCamRef = (refPosition - lowerCam).normalized;

            //float upperTheta = Vector2.SignedAngle(camForward.normalized, upperCamRef);

            //float lowerTheta = Vector2.SignedAngle(camForward.normalized, lowerCamRef);

            //Debug.Log($"Upper Theta: {upperTheta} | Lower Theta: {lowerTheta}");

            //return upperTheta <= 90 && lowerTheta >= -90;
            return false;
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