using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xami.Player {
    public class PlayerPhysics : MonoBehaviour
    {
        [Header("Components")]
        private Rigidbody rb;
        private Movement movementScript;
        [SerializeField] private Camera cam;

        [Header("Ground")]
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

        [SerializeField] private Vector3 groundColPos;
        [SerializeField] private float groundColRadius;

        [Header("Gravity")]
        [SerializeField] private float antigravitySpeed = 8f;
        public float AntigravitySpeed => antigravitySpeed;
        [SerializeField] private float gravityVelocity;
        [SerializeField] private float groundedVelocity;
        public float GravityVelocity => gravityVelocity;

        [Header("Jump")]
        private bool isJumping;
        public bool IsJumping
        {
            get { return isJumping; }
            set { isJumping = value; }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            movementScript = GetComponent<Movement>();

            if (cam == null)
            {
                cam = Camera.main;
            }

            Physics.gravity = new Vector3(0f, -gravityVelocity, 0f);
        }

        private void FixedUpdate()
        {
            GroundCheck();
            GravityManager();
            SlopeRotation();
        }

        private void GravityManager()
        {
            var groundCol = Physics.CheckSphere(transform.position + groundColPos, groundColRadius, groundLayer);

            if (((Mathf.Abs(movementScript.Velocity.x) > antigravitySpeed) || (Mathf.Abs(movementScript.Velocity.z) > antigravitySpeed)) && isGrounded && (!isJumping))
            {
                movementScript.Velocity = new Vector3(movementScript.Velocity.x, 0f, movementScript.Velocity.z);
            }
            
            else if ((groundCol) && (!isJumping))
            {
                movementScript.Velocity = new Vector3(movementScript.Velocity.x, -groundedVelocity, movementScript.Velocity.z);
            } 
            else
            {
                movementScript.Velocity -= new Vector3(0f, gravityVelocity, 0f) * Time.fixedDeltaTime;
            }
        }

        private void GroundCheck()
        {
            Debug.DrawRay(transform.position, -transform.up * groundRayDistance, Color.red, 1);
            if (Physics.Raycast(transform.position, -transform.up, out ray, groundRayDistance, groundLayer))
            {
                //Debug.Log("YES ground");
                isGrounded = true;
            } else
            {
                //Debug.Log("NO ground");
                isGrounded = false;
            }
        }

        private void SlopeRotation()
        {
            if (isGrounded)
            {
                groundNormal = ray.normal;
                Debug.DrawRay(ray.point, ray.normal * 2, Color.blue, 1);
                slopeRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation, slopeRotationSpeed * Time.fixedDeltaTime);

                if (Mathf.Round(new Vector3(slopeRotation.eulerAngles.x, 0f, slopeRotation.eulerAngles.z).magnitude) != 0)
                {
                    rb.MoveRotation(slopeRotation);
                } else
                {
                    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), slopeRotationSpeed * Time.fixedDeltaTime));
                }

            } else
            {
                
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), slopeRotationSpeed * Time.fixedDeltaTime));
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + groundColPos, groundColRadius);
        }
    }
}
