using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerPhysics : MonoBehaviour
    {
        [Header("Components")]
        private Rigidbody rb;
        private Movement movementScript;

        [Header("Ground Physics")]
        private bool isGrounded;
        public bool IsGrounded
        {
            get { return isGrounded; }
            set { isGrounded = value; }
        }
        private RaycastHit ray;
        [SerializeField] private float groundRayDistance = 1f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float slopeRotationSpeed = 90f;
        private Vector3 groundNormal;
        private Quaternion slopeRotation;
        [SerializeField] private Transform crashTransform;
        [SerializeField] private float crashRayDistance = 0.25f;

        [Header("Gravity Physics")]
        [SerializeField] private float antigravitySpeed = 8f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            movementScript = GetComponent<Movement>();
        }

        private void FixedUpdate()
        {
            GroundCheck();
            GravityManager();
            SlopeRotation();
            //Crash();
        }

        private void GravityManager()
        {
            if ((Mathf.Abs(movementScript.Velocity.x) > antigravitySpeed) || (Mathf.Abs(movementScript.Velocity.z) > antigravitySpeed) && isGrounded)
            {
                rb.useGravity = false;
            } else
            {
                rb.useGravity = true;
            }
        }

        private void Crash()
        {
            if (Physics.Raycast(crashTransform.position, movementScript.InputDirection, crashRayDistance)) {
                movementScript.Velocity = Vector3.zero;
            }
        }

        private void GroundCheck()
        {
            if (Physics.Raycast(transform.position, -transform.up, out ray, groundRayDistance, groundLayer))
            {
                isGrounded = true;
            } else
            {
                isGrounded = false;
            }
        }

        private void SlopeRotation()
        {
            if (isGrounded)
            {
                groundNormal = ray.normal;
                slopeRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

                //transform.rotation = slopeRotation;
                rb.MoveRotation(slopeRotation);
            } else
            {
                //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                rb.MoveRotation(Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f));
            }
        }
    }
}
