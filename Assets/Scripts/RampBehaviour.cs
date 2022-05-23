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

        [SerializeField]
        private bool activateOnlyOnce;

        private PlayerPhysics playerPhysicsScript;
        private Movement playerMovement;


        private void OnTriggerEnter(Collider collision)
        {

            if (collision.gameObject.tag == "Player")
            {   
                playerMovement = collision.gameObject?.GetComponent<Movement>();
                playerPhysicsScript = collision.gameObject?.GetComponent<PlayerPhysics>();
                playerMovement.CanMove = false;

                playerPhysicsScript.IsJumping = true;
                playerMovement.Velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (direction * force);
                StartCoroutine(WaitAndReactivatePlayerMovement());  
            }
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.tag == "Player")
            {
                playerMovement = collision.gameObject?.GetComponent<Movement>();
                playerPhysicsScript = collision.gameObject?.GetComponent<PlayerPhysics>();
                playerMovement.CanMove = false;

                playerPhysicsScript.IsJumping = true;
                playerMovement.Velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (direction * force);
                StartCoroutine(WaitAndReactivatePlayerMovement());
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position, direction * force);
        }

        private IEnumerator WaitAndReactivatePlayerMovement()
        {
            yield return new WaitForSeconds(reactivationTime);
            playerPhysicsScript.IsJumping = false;
            playerMovement.CanMove = true;
            if (activateOnlyOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
