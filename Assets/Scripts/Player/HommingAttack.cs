using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Player
{
    public class HommingAttack : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerPhysics playerPhysicsScript;
        [SerializeField] private Movement movementScript;
        [SerializeField] private InputActionReference jumpActionReference;
        [SerializeField] private Lock_Management lockScript;
        [SerializeField] private float jumpForce;
        private Vector3 hommingAttackDirection;
        private GameObject targetObject;
        public GameObject TargetObject { get { return targetObject; } set { targetObject = value; } }
        private bool activateHommingAttack;
        [SerializeField] private float hommingAttackSpeed = 10f;
        [SerializeField] private float preventiveTime = 5f;

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

        private void FixedUpdate()
        {
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
                rb.velocity = hommingAttackDirection * hommingAttackSpeed;
                StartCoroutine(PreventiveHommingAttackDeactivation());
            } else if ((!activateHommingAttack) && (targetObject != null))
            {
                hommingAttackDirection = targetObject.transform.position - transform.position;
            }
        }

        private IEnumerator PreventiveHommingAttackDeactivation()
        {
            yield return new WaitForSeconds(preventiveTime);
            activateHommingAttack = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.tag == "Enemy") && (activateHommingAttack))
            {
                StopAllCoroutines();
                playerPhysicsScript.IsJumping = true;
                rb.velocity = Vector3.zero;
                rb.velocity += transform.up * jumpForce;
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
            }
        }
    }
}
