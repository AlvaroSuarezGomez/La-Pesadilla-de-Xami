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
        private Vector3 inputDirection;
        private InputAction moveAction;

        [Header("Movement")]
        private Vector3 velocity;
        private Quaternion playerRotation;

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
        }

        private void MoveCharacter()
        {
            Vector2 dir = moveAction.ReadValue<Vector2>();
            inputDirection = new Vector3(dir.x, 0f, dir.y);

            playerRotation = Quaternion.LookRotation(Vector3.up, inputDirection);
            if (inputDirection.x != 0f || inputDirection.z != 0f)
            {
                transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * Quaternion.Euler(transform.rotation.eulerAngles.x, playerRotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
            }
            Accelerate();
            rb.velocity = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f) * new Vector3(velocity.x, rb.velocity.y, velocity.z);

        }

        public void Accelerate()
        {
            if ((inputDirection.x != 0f) && (Mathf.Abs(velocity.x) < maxSpeed))
            {
                velocity.x += acceleration * Time.deltaTime * inputDirection.x;
            }
            else if ((inputDirection.x == 0f) && (Mathf.Abs(velocity.x) > 0f))
            {
                if (velocity.x > 0f)
                {
                    velocity.x -= acceleration * Time.deltaTime;
                }
                else if (velocity.x < 0f)
                {
                    velocity.x += acceleration * Time.deltaTime;
                }
            }
            else
            {
                velocity.x = Mathf.Abs(velocity.x) * inputDirection.x;
            }

            if ((inputDirection.z != 0f) && (Mathf.Abs(velocity.z) < maxSpeed))
            {
                velocity.z += acceleration * Time.deltaTime * inputDirection.z;
            }
            else if ((inputDirection.z == 0f) && (Mathf.Abs(velocity.z) > 0f))
            {
                if (velocity.z > 0f)
                {
                    velocity.z -= acceleration * Time.deltaTime;
                }
                else if (velocity.z < 0f)
                {
                    velocity.z += acceleration * Time.deltaTime;
                }
            }
            else
            {
                velocity.z = Mathf.Abs(velocity.z) * inputDirection.z;
            }
        }
    }
}
