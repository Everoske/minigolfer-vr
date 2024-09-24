using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigolf.Putting.Interactable
{
    public class GolfBall : MonoBehaviour
    {
        [SerializeField]
        private float hitCooldownTime = 1f;

        private bool canBeHit = true;

        public UnityAction onBallHit;

        private void OnCollisionEnter(Collision other)
        {
            if (canBeHit && other.transform.TryGetComponent<GolfPutter>(out GolfPutter putter))
            {
                canBeHit = false;
                StartCoroutine(CanBeHitAgainCounter());
                onBallHit?.Invoke();
            }
        }

        private IEnumerator CanBeHitAgainCounter()
        {
            yield return new WaitForSeconds(hitCooldownTime);
            canBeHit = true;
        }
    }
}