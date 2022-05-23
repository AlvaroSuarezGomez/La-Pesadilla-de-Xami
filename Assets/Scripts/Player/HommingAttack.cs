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
        [SerializeField] private List<string> objectTags = new List<string>();
        private GameObject targetObject;
        public GameObject TargetObject { get { return targetObject; } set { targetObject = value; } }
        private bool activateHommingAttack;
        private bool inHommingAttack;
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
            PerformHommingAttack();
            //Debug.Log(inHommingAttack);
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if ((!playerPhysicsScript.IsGrounded) && (targetObject != null))
            {
                {
                    activateHommingAttack = true;
                }
            }
        }

        private void PerformHommingAttack()
        {
            if (activateHommingAttack && (targetObject != null) && !inHommingAttack)
            {
                inHommingAttack = true;
                movementScript.Velocity = Vector3.zero;
                StartCoroutine(MoveToObject());
                movementScript.CanMove = false;
                StartCoroutine(PreventiveHommingAttackDeactivation());
            }
        }

        private IEnumerator PreventiveHommingAttackDeactivation()
        {
            yield return new WaitForSeconds(preventiveTime);
            activateHommingAttack = false;
            inHommingAttack = false;
            movementScript.CanMove = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((objectTags.Contains(collision.gameObject.tag)) && (activateHommingAttack))
            {
                StopAllCoroutines();
                movementScript.CanMove = true;
                playerPhysicsScript.IsJumping = true;
                rb.velocity = Vector3.zero;
                rb.velocity += transform.up * jumpForce - transform.forward;
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
                inHommingAttack = false;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if ((objectTags.Contains(collision.gameObject.tag)) && (activateHommingAttack) && inHommingAttack)
            {
                StopAllCoroutines();
                movementScript.CanMove = true;
                playerPhysicsScript.IsJumping = true;
                rb.velocity = Vector3.zero;
                rb.velocity += transform.up * jumpForce;
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
                inHommingAttack = false;
            }
        }

        private IEnumerator MoveToObject()
        {
            while (inHommingAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, hommingAttackSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
