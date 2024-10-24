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
        private TextMeshProUGUI testDistanceTextL;

        private void Update()
        {
            //float distance3D = Vector3.Distance(leftHandRef.transform.position, playerCamera.transform.position);

            //if (distance3D <= visibleDistanceLimit )
            //{
            //    testDistanceTextL.text = "HELLO WORLD";
            //}
            //else
            //{
            //    testDistanceTextL.text = string.Empty;
            //}
            testDistanceTextL.text = $"X: {leftControllerTransform.localRotation.eulerAngles.x} Y: {leftControllerTransform.localRotation.eulerAngles.y} Z: {leftControllerTransform.localRotation.eulerAngles.z}";
        }


        // CamForward: Composed of either the x-y components of camera.forward or the x-z components of camera.forward
        // CamPosition: The cam's position clamped to x-y or x-z axes
        // RefPosition: The hand reference's position clamped to x-y or x-z axes
        // Intended Use: Call twice, once on the x-y axes and then on the x-z axes
        private bool LookingAtRef(Vector2 camForward, Vector2 camPosition, Vector2 refPosition)
        {
            // Pseudocode:
            // Create a Vector2D called camRef which is the vector between cam position and ref position
            // Using the camForward and camRef vectors, produce an angle showing their difference
            // If the angle is within acceptable limits return true
            // Otherwise, return false

            throw new NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(leftHandRef.transform.position, leftHandRef.transform.right, Color.blue);
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
        }
    }

}