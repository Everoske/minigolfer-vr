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

        private void OnDrawGizmos()
        {
            Debug.DrawRay(leftHandRef.transform.position, leftHandRef.transform.right, Color.blue);
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
        }
    }

}