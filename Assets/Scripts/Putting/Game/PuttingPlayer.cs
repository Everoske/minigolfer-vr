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
        private GolfBall playerGolfBall;
        // Consider making it so the GolfPutter class can be used to access its Grab Interactable class
        [SerializeField]
        private XRGrabInteractable putterPrefab;
        [SerializeField]
        private XRDirectInteractor rDirectInteractor;
        [SerializeField]
        private XRDirectInteractor lDirectInteractor;
        [SerializeField]
        private InputAction rightGripPressed;
        [SerializeField]
        private InputAction leftGripPressed;

        public GolfBall PlayerGolfBall => playerGolfBall;

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
        }

        private void SpawnPutter(IXRSelectInteractor handInteractor)
        {
            if (spawnedPutter != null) return;

            XRGrabInteractable putterInteractable = Instantiate(putterPrefab, handInteractor.transform.position, handInteractor.transform.rotation);
            spawnedPutter = putterInteractable.GetComponent<GolfPutter>();
            interactionManager.SelectEnter(handInteractor, putterInteractable);
        }

        private void PutterReleased()
        {
            if (spawnedPutter == null) return;
            if (spawnedPutter.IsSelected()) return;

            spawnedPutter.DespawnPutter();
            spawnedPutter = null;
        }

        private void SpawnPutterRightHand(InputAction.CallbackContext ctx)
        {
            if (rDirectInteractor.hasHover) return;
            if (rDirectInteractor.hasSelection) return;

            SpawnPutter(rDirectInteractor);
        }

        private void SpawnPutterLeftHand(InputAction.CallbackContext ctx)
        {
            if (lDirectInteractor.hasHover) return;
            if (lDirectInteractor.hasSelection) return;

            SpawnPutter(lDirectInteractor);
        }


        // Player Hits Select Button on XR Controller:
        // Player Holding Something => return;
        // Player is Already Holding Putter in Other Hand => return;
        // Player is Hovering Over and Interactable Object => return;

        // Instantiate Putter Prefab in Player Hand

        // In Putter Script:
        // If released by player, despawn after a set period of time.
    }
}
