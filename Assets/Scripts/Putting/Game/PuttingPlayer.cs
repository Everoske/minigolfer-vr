using Minigolf.Putting.Interactable;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Minigolf.Putting.Game
{
    public class PuttingPlayer : MonoBehaviour
    {
        [SerializeField]
        private XRInteractionManager interactionManager;

        [SerializeField]
        private GolfPutter putterPrefab;

        [SerializeField]
        private XRDirectInteractor rDirectInteractor;
        [SerializeField]
        private XRDirectInteractor lDirectInteractor;
        [SerializeField]
        private XRRayInteractor teleportInteractor;
        
        [SerializeField]
        private InputAction rightGripPressed;
        [SerializeField]
        private InputAction leftGripPressed;

        private GolfPutter spawnedPutter;

        private void Awake()
        {
            rightGripPressed.Enable();
            leftGripPressed.Enable();
        }

        private void OnEnable()
        {
            rightGripPressed.started += SpawnPutterRightHand;
            leftGripPressed.started += SpawnPutterLeftHand;
        }

        private void OnDisable()
        {
            rightGripPressed.started -= SpawnPutterRightHand;
            leftGripPressed.started -= SpawnPutterLeftHand;
            rightGripPressed.Disable();
            leftGripPressed.Disable();
        }

        private void Update()
        {
            if (!rDirectInteractor.hasSelection ||  !lDirectInteractor.hasSelection)
            {
                PutterReleased();
            }

            if (teleportInteractor.isActiveAndEnabled && spawnedPutter != null)
            {
                DespawnPutter();
            }
        }

        private void SpawnPutter(IXRSelectInteractor handInteractor)
        {
            if (spawnedPutter != null) return;

            spawnedPutter = Instantiate(putterPrefab, handInteractor.transform.position, handInteractor.transform.rotation);
            interactionManager.SelectEnter(handInteractor, spawnedPutter.PutterInteractable);
        }

        private void PutterReleased()
        {
            if (spawnedPutter == null) return;
            if (spawnedPutter.IsSelected()) return;

            DespawnPutter();
        }

        private void DespawnPutter()
        {
            spawnedPutter.DespawnPutter();
            spawnedPutter = null;
        }

        private void SpawnPutterRightHand(InputAction.CallbackContext ctx)
        {
            if (rDirectInteractor.hasHover) return;
            if (rDirectInteractor.hasSelection) return;
            if (teleportInteractor.isActiveAndEnabled) return;

            SpawnPutter(rDirectInteractor);
        }

        private void SpawnPutterLeftHand(InputAction.CallbackContext ctx)
        {
            if (lDirectInteractor.hasHover) return;
            if (lDirectInteractor.hasSelection) return;
            if (teleportInteractor.isActiveAndEnabled) return;

            SpawnPutter(lDirectInteractor);
        }
    }
}
