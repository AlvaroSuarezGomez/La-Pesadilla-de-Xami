using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class RampBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float force;

        [SerializeField]
        private Vector3 direction;

        [SerializeField]
        private float reactivationTime;

        private PlayerPhysics playerPhysicsScript;
        private Movement playerMovement;


        private void OnCollisionEnter(Collision collision)
        {
            playerMovement = collision.gameObject?.GetComponent<Movement>();
            playerPhysicsScript = collision.gameObject?.GetComponent<PlayerPhysics>();
            playerMovement.CanMove = false;

            playerPhysicsScript.IsJumping = true;
            playerMovement.Velocity = Vector3.zero;
            playerMovement.Velocity += (direction * force);
            StartCoroutine(WaitAndReactivatePlayerMovement());

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position, direction * force);
        }

        private IEnumerator WaitAndReactivatePlayerMovement()
        {
            yield return new WaitForSeconds(reactivationTime);
            playerPhysicsScript.IsJumping = false;
            playerMovement.Velocity = Vector3.zero;
            playerMovement.CanMove = true;
        }
    }
}
