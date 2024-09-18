using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Minigolf.XR
{
    public class RayHover : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private XRInteractorLineVisual lineVisual;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineVisual = GetComponent<XRInteractorLineVisual>();
        }

        public void DisableRay()
        {
            lineRenderer.enabled = false;
            lineVisual.enabled = false;
        }

        public void EnableRay()
        {
            lineRenderer.enabled = true;
            lineVisual.enabled = true;
        }
    }
}
