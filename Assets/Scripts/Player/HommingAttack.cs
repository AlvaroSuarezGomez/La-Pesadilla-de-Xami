using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Player
{
    public class HommingAttack : MonoBehaviour
    {
        [SerializeField] private PlayerPhysics playerPhysicsScript;
        [SerializeField] private Movement movementScript;
        [SerializeField] private InputActionReference jumpActionReference;
        [SerializeField] private Lock_Management lockScript;
        [SerializeField] private float jumpForce;
        private GameObject targetObject;
        private bool activateHommingAttack;
        [SerializeField] private float hommingAttackSpeed = 10f;

        private void Awake()
        {
            if (playerPhysicsScript == null)
            {
                playerPhysicsScript = GetComponent<PlayerPhysics>();
            }

            if (movementScript == null)
            {
                movementScript = GetComponent<Movement>();
            }

            jumpActionReference.action.performed += Action_performed;
        }

        private void Update()
        {
            targetObject = lockScript.ColObject;
            MoveTowardsObject();
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if ((!playerPhysicsScript.IsGrounded) && (targetObject != null))
            {
                activateHommingAttack = true;
            }
        }

        private void MoveTowardsObject()
        {
            if ((activateHommingAttack) && (targetObject != null))
            {
                movementScript.Velocity = Vector3.zero;
                transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, hommingAttackSpeed * Time.deltaTime);
                StartCoroutine(PreventiveHommingAttackDeactivation());
            }
        }

        private IEnumerator WaitAndDeactivateJump()
        {
            yield return new WaitForSeconds(0.25f);
            playerPhysicsScript.IsJumping = false;

        }

        private IEnumerator PreventiveHommingAttackDeactivation()
        {
            yield return new WaitForSeconds(2f);
            activateHommingAttack = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.tag == "Enemy") && (activateHommingAttack))
            {
                playerPhysicsScript.IsJumping = true;
                movementScript.Velocity = new Vector3(movementScript.Velocity.x, 0f, movementScript.Velocity.z);
                movementScript.Velocity += transform.up * jumpForce;
                activateHommingAttack = false;
                StartCoroutine(WaitAndDeactivateJump());
            }
        }
    }
}
