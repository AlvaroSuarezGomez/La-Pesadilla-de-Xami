using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Movement movementScript;
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

            if (movementScript == null)
            {
                movementScript = GetComponent<Movement>();
            }

            jumpActionReference.action.performed += Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if (playerPhysicsScript.IsGrounded)
            {
                playerPhysicsScript.IsJumping = true;
                rb.velocity += transform.up * jumpForce;
                StartCoroutine(WaitAndDeactivateJump());
            }
        }

        private IEnumerator WaitAndDeactivateJump()
        {
            yield return new WaitForSeconds(0.25f);
            playerPhysicsScript.IsJumping = false; ;

        }
    }
}
