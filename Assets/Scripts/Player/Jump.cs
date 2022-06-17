using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Xami.Player
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

        [Header("Animation")]
        [SerializeField] private Animator anim;
        [SerializeField] private int jumpIndex = Animator.StringToHash("Jump");

        private void Awake()
        {
            if (anim == null)
            {
                anim = GetComponentInChildren<Animator>();
            }

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

        private void OnDestroy()
        {
            jumpActionReference.action.performed -= Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if (playerPhysicsScript.IsGrounded && movementScript.CanMove)
            {
                anim.SetTrigger(jumpIndex);
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
