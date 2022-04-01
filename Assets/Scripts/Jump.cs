using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerPhysics playerPhysicsScript;
        [SerializeField] private Rigidbody rb;

        [Header("Input")]
        [SerializeField] private InputActionReference jumpActionReference;

        [Header("Jump Physics")]
        [SerializeField] private float jumpForce = 10f;

        private void Awake()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            jumpActionReference.action.performed += Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            // Si el personaje está en el suelo y el jugador pulsa espacio...
            if (playerPhysicsScript.IsGrounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
    }
}
