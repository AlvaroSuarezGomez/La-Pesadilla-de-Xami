using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Camera cam;
        [SerializeField] private PlayerPhysics physicsScript;

        [Header("Input")]
        [SerializeField] private InputActionAsset input;
        private InputAction moveAction;
        private Vector3 inputDirection;
        public Vector3 InputDirection { get { return inputDirection; } }

        [Header("Movement")]
        private bool canMove = true;
        public bool CanMove { get { return canMove; } set { canMove = value; } }
        private Vector3 velocity;
        
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private Vector3 localVelocity;

        private Quaternion playerRotation;
        [SerializeField] private float rotationSpeed = 90f;

        [Header("Speed Physics")]
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float maxSpeed = 10f;

        [Header("Model Settings")]
        [SerializeField] private Transform playerModel;

        private void Awake()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            if (cam == null)
            {
                cam = Camera.main;
            }

            if (physicsScript == null)
            {
                physicsScript = GetComponent<PlayerPhysics>();
            }

            input.Enable();
            moveAction = input.FindAction("Move");
        }

        private void FixedUpdate()
        {
                MoveCharacter();
        }

        private void MoveCharacter()
        {
            Vector2 dir = moveAction.ReadValue<Vector2>();
            inputDirection = new Vector3(dir.x, 0f, dir.y);

            playerRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            if (inputDirection.x != 0f || inputDirection.z != 0f)
            {
                playerModel.rotation = transform.rotation * playerRotation;
                //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }

            velocity = rb.velocity;
            localVelocity = transform.InverseTransformDirection(velocity);

            if (canMove)
            {
                localVelocity.x = Accelerate(inputDirection.x, localVelocity.x);
                localVelocity.z = Accelerate(inputDirection.z, localVelocity.z);
            }

            velocity = transform.TransformDirection(localVelocity);
            rb.velocity = velocity;
        }

        public float Accelerate(float dir, float vel)
        {
            if ((dir != 0f) && (Mathf.Abs(vel) < maxSpeed))
            {
                vel += acceleration * Time.fixedDeltaTime * dir;
            }
            else if ((dir == 0f) && (Mathf.Abs(vel) > 0f))
            {
                if (vel > 0f)
                {
                    vel -= acceleration * Time.fixedDeltaTime;
                }
                else if (vel < 0f)
                {
                    vel += acceleration * Time.fixedDeltaTime;
                }
            }
            else
            {
                vel = Mathf.Abs(vel) * dir;
            }

            return vel;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.eulerAngles.y, transform.rotation.eulerAngles.z) * velocity);
        }
    }
}
