using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Minigolf.XR
{
    public class PressableButton : MonoBehaviour
    {
        [SerializeField]
        private Transform visualComponent;
        [SerializeField]
        private Transform visualParent;
        [SerializeField]
        private Vector3 localEndPosition;

        [Range(0f, 1f)]
        [SerializeField]
        private float pressedThreshold = 0.15f;

        [SerializeField]
        private float springBackTime = 0.5f;
        [SerializeField]
        private float deadtime = 0.25f;

        public UnityEvent onPressed;
        public UnityEvent onReleased;

        private Vector3 localDefaultPosition;
        private Vector3 pathNormal;
        private Vector3 movement;

        private XRBaseInteractable interactable;
        private Transform pokeTransform;

        private bool isInteracting = false;
        private bool isPressed = false;
        private bool canTriggerPressed = true;

        private float springCounter = 0.0f;

        private void Awake()
        {
            interactable = GetComponent<XRBaseInteractable>();
        }

        private void OnEnable()
        {
            interactable.hoverEntered.AddListener(HoverEntered);
            interactable.hoverExited.AddListener(HoverExited);
        }

        private void OnDisable()
        {
            interactable.hoverEntered.RemoveAllListeners();
            interactable.hoverExited.RemoveAllListeners();
        }

        private void Start()
        {
            localDefaultPosition = visualComponent.localPosition;
            pathNormal = (localEndPosition - localDefaultPosition).normalized;
        }

        private void Update()
        {
            if (isInteracting)
            {
                MoveVisual();
            }
            else if (visualComponent.localPosition != localDefaultPosition)
            {
                SpringBack();
            }

            CheckPressed();
        }

        private void HoverEntered(BaseInteractionEventArgs hover)
        {
            if (hover.interactorObject is XRPokeInteractor)
            {
                XRPokeInteractor pokeInteractor = (XRPokeInteractor) hover.interactorObject;
                isInteracting = true;
                pokeTransform = pokeInteractor.transform;
                movement = visualComponent.position - pokeTransform.position;
            }
        }

        private void HoverExited(BaseInteractionEventArgs hover)
        {
            isInteracting = false;
        }

        private void MoveVisual()
        {
            Vector3 localTargetPosition = visualComponent.InverseTransformPoint(pokeTransform.position + movement);
            localTargetPosition = Vector3.Project(localTargetPosition, pathNormal);
            visualComponent.position = ClampedTargetPosition(visualComponent.TransformPoint(localTargetPosition));
        }

        private Vector3 ClampedTargetPosition(Vector3 targetPosition)
        {
            Vector3 defaultPosition = visualParent.TransformPoint(localDefaultPosition);
            Vector3 endPosition = visualParent.TransformPoint(localEndPosition);

            float x = Mathf.Clamp(
                targetPosition.x, 
                Mathf.Min(defaultPosition.x, endPosition.x), 
                Mathf.Max(defaultPosition.x, endPosition.x)
                );

            float y = Mathf.Clamp(
                targetPosition.y,
                Mathf.Min(defaultPosition.y, endPosition.y),
                Mathf.Max(defaultPosition.y, endPosition.y)
                );

            float z = Mathf.Clamp(
                targetPosition.z,
                Mathf.Min(defaultPosition.z, endPosition.z),
                Mathf.Max(defaultPosition.z, endPosition.z)
                );

            return new Vector3(x, y, z);
        }

        private void SpringBack()
        {
            springCounter += Time.deltaTime;
            visualComponent.localPosition =
                Vector3.Lerp(visualComponent.localPosition, localDefaultPosition, springCounter / springBackTime);

            if (visualComponent.localPosition == localDefaultPosition)
            {
                springCounter = 0;
            }
        }

        private void CheckPressed()
        {
            float percentDistance = 
                (visualComponent.localPosition - localEndPosition).magnitude / (localDefaultPosition - localEndPosition).magnitude;

            if (percentDistance <= pressedThreshold)
            {
                if (canTriggerPressed)
                {
                    onPressed?.Invoke();
                }
                canTriggerPressed = false;
                isPressed = true;
            }
            else
            {
                if (isPressed)
                {
                    onReleased?.Invoke();
                    StartCoroutine(ProcessDeadtime());
                }
                isPressed = false;
            }
        }

        private IEnumerator ProcessDeadtime()
        {
            yield return new WaitForSeconds(deadtime);
            if (!isPressed)
            {
                canTriggerPressed = true;
            }
        }
    }
}