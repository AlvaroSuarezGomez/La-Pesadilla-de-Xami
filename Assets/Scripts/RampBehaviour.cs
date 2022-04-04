using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampBehaviour : MonoBehaviour
{
    [SerializeField]
    private float force;

    [SerializeField]
    private Vector3 direction;


    private void OnCollisionEnter(Collision collision)
    {
        var pRigidBody = collision.gameObject?.GetComponent<Rigidbody>();

        pRigidBody.AddForce(direction * force);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.position + direction);
    }
}
