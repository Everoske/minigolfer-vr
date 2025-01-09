using Minigolf.Putting.Interactable;
using Minigolf.Scriptable;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Minigolf.Putting.Game
{
    public class PuttingPlayer : MonoBehaviour
    {
        [SerializeField]
        private XRInteractionManager interactionManager;

        [SerializeField]
        private GolfPutter putterPrefab;
        [SerializeField]
        private GolfBall ballPrefab;

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

        public UnityAction<GolfBallTemplate, bool> onBallChanged;

        private GolfPutter spawnedPutter;
        private GolfBall spawnedBall;
        private GolfBallTemplate assignedGBTemplate;

        private bool canChangeBall = true;

        private void Awake()
        {
            rightGripPressed.Enable();
            leftGripPressed.Enable();
        }

        private void OnEnable()
        {
            rightGripPressed.started += SpawnPutterRightHand;
            leftGripPressed.started += SpawnPutterLeftHand;
            lDirectInteractor.selectEntered.AddListener(AssignBall);
            rDirectInteractor.selectEntered.AddListener(AssignBall);
        }

        private void OnDisable()
        {
            rightGripPressed.started -= SpawnPutterRightHand;
            leftGripPressed.started -= SpawnPutterLeftHand;
            rightGripPressed.Disable();
            leftGripPressed.Disable();
            lDirectInteractor.selectEntered.RemoveAllListeners();
            rDirectInteractor.selectEntered.RemoveAllListeners();
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

        /// <summary>
        /// Change whether the player is allowed to change their ball
        /// </summary>
        /// <param name="canChangeBall"></param>
        public void SetCanChangeBall(bool canChangeBall)
        {
            this.canChangeBall = canChangeBall;
        }

        /// <summary>
        /// Determines if the player has a ball prefab assigned
        /// </summary>
        /// <returns></returns>
        public bool HasGolfBall()
        {
            return assignedGBTemplate != null;
        }

        /// <summary>
        /// Spawns the player's golf ball at a given position
        /// and assigns an onHitEvent to that golf ball
        /// </summary>
        /// <param name="position">Position to spawn ball</param>
        /// <param name="onHitEvent">Event triggered when ball hit</param>
        public void SpawnBall(Vector3 position, UnityAction onHitEvent)
        {
            if (assignedGBTemplate == null) return;

            if (spawnedBall != null)
            {
                DespawnBall(onHitEvent);
            }

            spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity);
            spawnedBall.AssignMaterial(assignedGBTemplate.material);
            spawnedBall.onBallHit += onHitEvent;
            spawnedBall.IsAssigned = true;
        }

        /// <summary>
        /// Despawns the player's golf ball and unassigns
        /// the onHitEvent 
        /// </summary>
        /// <param name="onHitEvent">Event triggered when ball hit</param>
        public void DespawnBall(UnityAction onHitEvent)
        {
            if (spawnedBall == null) return;

            spawnedBall.onBallHit -= onHitEvent;
            Destroy(spawnedBall.gameObject);
            spawnedBall = null;
        }

        /// <summary>
        /// Assigns a ball to the player when it is picked up
        /// </summary>
        /// <param name="select">On Selection Event</param>
        private void AssignBall(BaseInteractionEventArgs select)
        {
            if (!canChangeBall) return;

            if (select.interactableObject.transform.TryGetComponent<GolfBall>(out GolfBall heldBall))
            {
                if (spawnedBall != null)
                {
                    spawnedBall.IsAssigned = false;
                }

                heldBall.IsAssigned = true;
                spawnedBall = heldBall;
                assignedGBTemplate = heldBall.Template;
                onBallChanged?.Invoke(heldBall.Template, true);
            }
        }

        /// <summary>
        /// Spawns the putter in the player's hand when selection pressed
        /// </summary>
        /// <param name="handInteractor">Hand interactor to spawn putter in</param>
        private void SpawnPutter(IXRSelectInteractor handInteractor)
        {
            if (spawnedPutter != null) return;

            spawnedPutter = Instantiate(putterPrefab, handInteractor.transform.position, handInteractor.transform.rotation);
            interactionManager.SelectEnter(handInteractor, spawnedPutter.PutterInteractable);
        }

        /// <summary>
        /// Despawns putter when selection released by player
        /// </summary>
        private void PutterReleased()
        {
            if (spawnedPutter == null) return;
            if (spawnedPutter.IsSelected()) return;

            DespawnPutter();
        }

        /// <summary>
        /// Despawn putter
        /// </summary>
        private void DespawnPutter()
        {
            spawnedPutter.DespawnPutter();
            spawnedPutter = null;
        }

        /// <summary>
        /// Spawn putter in the player's right hand
        /// </summary>
        /// <param name="ctx"></param>
        private void SpawnPutterRightHand(InputAction.CallbackContext ctx)
        {
            if (rDirectInteractor.hasHover) return;
            if (rDirectInteractor.hasSelection) return;
            if (teleportInteractor.isActiveAndEnabled) return;

            SpawnPutter(rDirectInteractor);
        }

        /// <summary>
        /// Spawn putter in the player's left hand
        /// </summary>
        /// <param name="ctx"></param>
        private void SpawnPutterLeftHand(InputAction.CallbackContext ctx)
        {
            if (lDirectInteractor.hasHover) return;
            if (lDirectInteractor.hasSelection) return;
            if (teleportInteractor.isActiveAndEnabled) return;

            SpawnPutter(lDirectInteractor);
        }
    }
}
