using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Minigolf.Putting.Interactable
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class GolfPutter : MonoBehaviour
    {
        private bool isHeld = false;

        private XRGrabInteractable putterInteractable;

        public bool IsHeld
        {
            get => isHeld;
            set => isHeld = value;
        }

        public XRGrabInteractable PutterInteractable => putterInteractable;

        private void Awake()
        {
            putterInteractable = GetComponent<XRGrabInteractable>();
        }

        public bool IsSelected() => putterInteractable.isSelected;

        public void DespawnPutter()
        {
            Destroy(gameObject);
        }
    }
}
