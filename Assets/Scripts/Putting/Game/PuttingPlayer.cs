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

        // IMPORTANT: PLEASE READ!!!
        // Problems and Suggestions:
        // - Problems:
        // --> If player uses the ray interactor to teleport, the golf putter spawns and stays in the player's hand even after the grip button is released
        // - Suggestions:
        // --> Implement a PlayerInteractController class to manage player interactors
        // --> Make it so the player cannot spawn a golf putter when using the ray interactor or not using the direct interactor
        // --> Putter should disappear when teleporting or when the ray interactor is called
        // --> Simplify code as much as possible
        // --> Make it so the GolfPutter class is used for the prefab. Make the Grab Interactable of the GolfPutter publically accessible

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
    }
}
