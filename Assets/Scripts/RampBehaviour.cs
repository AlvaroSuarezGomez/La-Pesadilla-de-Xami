using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xami.Player;
using Xami.Vehicles;

namespace Xami.Props
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

        [SerializeField]
        private Transform centerTransform;

        [SerializeField] private bool center;

        private PlayerPhysics playerPhysicsScript;
        private Movement playerMovement;
        private Vehicle vehicleScript;

        private void Awake()
        {
            if (centerTransform == null)
            {
                centerTransform = transform;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {

            if (collision.gameObject.tag == "Player")
            {   
                playerMovement = collision.gameObject?.GetComponent<Movement>();
                playerPhysicsScript = collision.gameObject?.GetComponent<PlayerPhysics>();

                playerPhysicsScript.IsJumping = true;
                playerMovement.Velocity = Vector3.zero;
                //Debug.Log("colision");
                if (centerTransform != null && center)
                {
                    collision.gameObject.transform.position = centerTransform.position;
                }
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (direction * force);
                StartCoroutine(WaitAndReactivatePlayerMovement());  
            }
            else if (collision.gameObject.tag == "Vehicle")
            {
                Debug.Log("Vehiculo");
                vehicleScript = collision.gameObject.GetComponent<Vehicle>();
                if (centerTransform != null && center)
                {
                    collision.gameObject.transform.position = centerTransform.position;
                }
                StartCoroutine(WaitAndReactivateVehicle());
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (direction * force);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.tag == "Player")
            {
                playerMovement = collision.gameObject?.GetComponent<Movement>();
                playerPhysicsScript = collision.gameObject?.GetComponent<PlayerPhysics>();

                playerPhysicsScript.IsJumping = true;
                playerMovement.Velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (direction * force);
                if (centerTransform != null && center)
                {
                    collision.gameObject.transform.position = centerTransform.position;
                }
                StartCoroutine(WaitAndReactivatePlayerMovement());
            } else if (collision.gameObject.tag == "Vehicle")
            {
                Debug.Log("Vehiculo");
                vehicleScript = collision.gameObject.GetComponent<Vehicle>();
                StartCoroutine(WaitAndReactivateVehicle());
                if (centerTransform != null && center)
                {
                    collision.gameObject.transform.position = centerTransform.position;
                }
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (direction * force);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position, direction * force);
        }

        private IEnumerator WaitAndReactivatePlayerMovement()
        {
            playerMovement.DisableMovementForTime(reactivationTime);
            yield return new WaitForSeconds(reactivationTime);
            playerPhysicsScript.IsJumping = false;
            if (activateOnlyOnce)
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator WaitAndReactivateVehicle()
        {
            vehicleScript.Activated = false;
            yield return new WaitForSeconds(reactivationTime);
            vehicleScript.Activated = true;
            if (activateOnlyOnce)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(centerTransform.position, centerTransform.localScale);
        }
    }
}
