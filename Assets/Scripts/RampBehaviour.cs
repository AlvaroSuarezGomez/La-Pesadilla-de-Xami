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


        private void OnCollisionEnter(Collision collision)
        {
            var playerMovement = collision.gameObject?.GetComponent<Movement>();

            playerMovement.Velocity += (direction * force);

            Debug.Log("AAAA");

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position, direction * force);
        }
    }
}
