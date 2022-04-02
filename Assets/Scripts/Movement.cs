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

        [Header("Input")]
        [SerializeField] private InputActionAsset input;
        private InputAction moveAction;
        private Vector3 inputDirection;
        public Vector3 InputDirection { get { return inputDirection; } }

        [Header("Movement")]
        private Vector3 velocity;
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        private Quaternion playerRotation;
        [SerializeField] private float rotationSpeed = 90f;

        [Header("Speed Physics")]
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float maxSpeed = 10f;

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

            input.Enable();
            moveAction = input.FindAction("Move");
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            Debug.Log(rb.velocity);
        }

        private void MoveCharacter()
        {
            Vector2 dir = moveAction.ReadValue<Vector2>();
            inputDirection = new Vector3(dir.x, 0f, dir.y);

            /*playerRotation = Quaternion.LookRotation(transform.up, inputDirection);
            if (inputDirection.x != 0f || inputDirection.z != 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, playerRotation.eulerAngles.y, transform.rotation.eulerAngles.z), rotationSpeed * Time.fixedDeltaTime);
            }*/
            Accelerate();
            rb.velocity = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        public void Accelerate()
        {
            if ((inputDirection.x != 0f) && (Mathf.Abs(velocity.x) < maxSpeed))
            {
                velocity.x += acceleration * Time.fixedDeltaTime * inputDirection.x;
            }
            else if ((inputDirection.x == 0f) && (Mathf.Abs(velocity.x) > 0f))
            {
                if (velocity.x > 0f)
                {
                    velocity.x -= acceleration * Time.fixedDeltaTime;
                }
                else if (velocity.x < 0f)
                {
                    velocity.x += acceleration * Time.fixedDeltaTime;
                }
            }
            else
            {
                velocity.x = Mathf.Abs(velocity.x) * inputDirection.x;
            }

            if ((inputDirection.z != 0f) && (Mathf.Abs(velocity.z) < maxSpeed))
            {
                velocity.z += acceleration * Time.fixedDeltaTime * inputDirection.z;
            }
            else if ((inputDirection.z == 0f) && (Mathf.Abs(velocity.z) > 0f))
            {
                if (velocity.z > 0f)
                {
                    velocity.z -= acceleration * Time.fixedDeltaTime;
                }
                else if (velocity.z < 0f)
                {
                    velocity.z += acceleration * Time.fixedDeltaTime;
                }
            }
            else
            {
                velocity.z = Mathf.Abs(velocity.z) * inputDirection.z;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.eulerAngles.y, transform.rotation.eulerAngles.z) * velocity);
        }
    }
}
