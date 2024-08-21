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
        [SerializeField]
        private XRGrabInteractable putterPrefab;
        [SerializeField]
        private XRDirectInteractor rDirectInteractor;
        [SerializeField]
        private InputAction rightGripPressed;

        public GolfBall PlayerGolfBall => playerGolfBall;

        private void OnEnable()
        {
            rightGripPressed.Enable();
            rightGripPressed.started += SpawnPutterRightHand;
        }

        private void OnDisable()
        {
            rightGripPressed.started -= SpawnPutterRightHand;
            rightGripPressed.Disable();
        }

        private void SpawnPutterRightHand(InputAction.CallbackContext ctx)
        {
            if (rDirectInteractor.hasHover) return;
            if (rDirectInteractor.hasSelection) return;

            XRGrabInteractable spawnedPutter = Instantiate(putterPrefab, rDirectInteractor.transform.position, rDirectInteractor.transform.rotation);
            interactionManager.SelectEnter(rDirectInteractor, spawnedPutter);
            
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
