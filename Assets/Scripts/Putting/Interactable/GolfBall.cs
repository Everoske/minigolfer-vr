using Minigolf.Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Minigolf.Putting.Interactable
{
    /// <summary>
    /// Mini Golf Ball
    /// </summary>
    public class GolfBall : MonoBehaviour
    {
        [SerializeField]
        private float hitCooldownTime = 1f;
        [SerializeField]
        private float unassignedDespawnTime = 120f;
        [SerializeField]
        private GolfBallTemplate template;

        private bool canBeHit = true;
        private bool isAssigned = false;
        private bool isHeld = false;

        private float despawnTimer = 0f;

        private XRGrabInteractable interactable;
        private MeshRenderer meshRenderer;

        public UnityAction onBallHit;

        public bool IsAssigned 
        {
            get => isAssigned;
            set
            {
                isAssigned = value;
                despawnTimer = 0.0f;
            }
        }

        public GolfBallTemplate Template
        {
            get => template;
            set  
            { 
                template = value;
                meshRenderer.material = template.material;
            }
        }

        private void Awake()
        {
            interactable = GetComponent<XRGrabInteractable>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            interactable.selectEntered.AddListener(BallHeld);
            interactable.selectExited.AddListener(BallReleased);
        }

        private void OnDisable()
        {
            interactable.selectEntered.RemoveAllListeners();
            interactable.selectExited.RemoveAllListeners();
        }

        private void Update()
        {
            ProcessDespawnTime();
        }

        private void OnCollisionEnter(Collision other)
        {
            bool isGolfPutter = other.transform.TryGetComponent<GolfPutter>(out GolfPutter putter);
            if (!isGolfPutter) return;

            if (canBeHit)
            {
                canBeHit = false;
                StartCoroutine(CanBeHitAgainCounter());
                onBallHit?.Invoke();
            }

            despawnTimer = 0.0f;
        }

        /// <summary>
        /// Assigns a material to the golf ball
        /// </summary>
        /// <param name="materialToAssign">Golf ball material</param>
        public void AssignMaterial(Material materialToAssign)
        {
            meshRenderer.material = materialToAssign;
        }

        /// <summary>
        /// Returns the ball's assigned material
        /// </summary>
        /// <returns>Mini Golf Ball's material</returns>
        public Material GetAssignedMaterial()
        {
            return meshRenderer.material;
        }

        /// <summary>
        /// Prevent ball from despawning when held
        /// </summary>
        /// <param name="select"></param>
        private void BallHeld(BaseInteractionEventArgs select)
        {
            isHeld = true;
            despawnTimer = 0.0f;
        }

        /// <summary>
        /// Allow the ball to despawn when released
        /// </summary>
        /// <param name="select"></param>
        private void BallReleased(BaseInteractionEventArgs select)
        {
            isHeld = false;
        }

        /// <summary>
        /// Stop incrementing the hit counter for a set period of time
        /// </summary>
        /// <returns></returns>
        private IEnumerator CanBeHitAgainCounter()
        {
            yield return new WaitForSeconds(hitCooldownTime);
            canBeHit = true;
        }

        /// <summary>
        /// Increment despawn timer when ball is unassigned or
        /// not held by player. Despawn ball after a set period
        /// of time
        /// </summary>
        private void ProcessDespawnTime()
        {
            if (isAssigned) return;
            if (isHeld) return;

            despawnTimer += Time.deltaTime;
            if (despawnTimer >= unassignedDespawnTime)
            {
                Destroy(gameObject);
            }
        }
    }
}