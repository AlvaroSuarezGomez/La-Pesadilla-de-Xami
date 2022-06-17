using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Xami.Player
{
    public class HommingAttack : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerPhysics playerPhysicsScript;
        [SerializeField] private Movement movementScript;
        [SerializeField] private InputActionReference jumpActionReference;
        [SerializeField] private Lock_Management lockScript;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private float jumpForce;
        private Vector3 hommingAttackDirection;
        [SerializeField] private List<string> objectTags = new List<string>();
        [SerializeField] private List<string> dontJumpTags = new List<string>();
        private GameObject targetObject;
        public GameObject TargetObject { get { return targetObject; } set { targetObject = value; } }
        [SerializeField]
        private Collider hommingAttackCollider;
        private bool activateHommingAttack;
        private bool inHommingAttack;
        public bool InHommingAttack { get { return InHommingAttack; } set { inHommingAttack = value; } }
        [SerializeField] private float hommingAttackSpeed = 10f;
        [SerializeField] private float preventiveTime = 5f;

        [SerializeField]
        private Animator anim;
        private int inHommingAttackIndex = Animator.StringToHash("InHommingAttack");

        private void Awake()
        {
            if (anim == null)
            {
                anim = GetComponentInChildren<Animator>();
            }

            if (playerHealth == null)
            {
                playerHealth = GetComponent<PlayerHealth>();
            }

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

        private void OnDestroy()
        {
            jumpActionReference.action.performed -= Action_performed;
        }

        private void FixedUpdate()
        {
            PerformHommingAttack();
            
            //Debug.Log(inHommingAttack);
        }

        private void Update()
        {
            anim.SetBool(inHommingAttackIndex, inHommingAttack);
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if ((!playerPhysicsScript.IsGrounded) && (targetObject != null) && movementScript.CanMove)
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
                hommingAttackCollider.enabled = false;
                hommingAttackDirection = targetObject.transform.position - transform.position;
                movementScript.CanMove = false;
                inHommingAttack = true;
                movementScript.Velocity = Vector3.zero;
                playerHealth.IsAttacking = true;
                
                StartCoroutine(MoveToObject());
                StartCoroutine(PreventiveHommingAttackDeactivation());
            }
        }

        private IEnumerator PreventiveHommingAttackDeactivation()
        {
            yield return new WaitForSeconds(preventiveTime);
            hommingAttackCollider.enabled = true;
            movementScript.CanMove = true;
            activateHommingAttack = false;
            inHommingAttack = false; 
            playerHealth.IsAttacking = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((objectTags.Contains(collision.gameObject.tag)) && (activateHommingAttack))
            {
                hommingAttackCollider.enabled = true;
                StopAllCoroutines();
                movementScript.CanMove = true;
                playerPhysicsScript.IsJumping = true;
                if (!dontJumpTags.Contains(collision.gameObject.tag))
                {
                    rb.velocity = Vector3.zero;
                    rb.velocity += transform.up * jumpForce - transform.forward;
                }
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
                inHommingAttack = false;
                playerHealth.IsAttacking = false;
                playerHealth.AttackTime = 0.1f;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if ((objectTags.Contains(collision.gameObject.tag)) && (activateHommingAttack) && inHommingAttack)
            {
                hommingAttackCollider.enabled = true;
                StopAllCoroutines();
                movementScript.CanMove = true;
                playerPhysicsScript.IsJumping = true;
                if (!dontJumpTags.Contains(collision.gameObject.tag))
                {
                    rb.velocity = Vector3.zero;
                    rb.velocity += transform.up * jumpForce;
                }
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
                inHommingAttack = false;
                playerHealth.IsAttacking = false;
                playerHealth.AttackTime = 0.1f;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if ((objectTags.Contains(collision.gameObject.tag)) && (activateHommingAttack))
            {
                hommingAttackCollider.enabled = true;
                StopAllCoroutines();
                movementScript.CanMove = true;
                playerPhysicsScript.IsJumping = true;
                if (!dontJumpTags.Contains(collision.gameObject.tag))
                {
                    rb.velocity = Vector3.zero;
                    rb.velocity += transform.up * jumpForce - transform.forward;
                }
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
                inHommingAttack = false;
                playerHealth.IsAttacking = false;
                playerHealth.AttackTime = 0.1f;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if ((objectTags.Contains(collision.gameObject.tag)) && (activateHommingAttack) && inHommingAttack)
            {
                hommingAttackCollider.enabled = true;
                StopAllCoroutines();
                movementScript.CanMove = true;
                playerPhysicsScript.IsJumping = true;
                if (!dontJumpTags.Contains(collision.gameObject.tag))
                {
                    rb.velocity = Vector3.zero;
                    rb.velocity += transform.up * jumpForce;
                }
                activateHommingAttack = false;
                playerPhysicsScript.IsJumping = false;
                inHommingAttack = false;
                playerHealth.IsAttacking = false;
                playerHealth.AttackTime = 0.1f;
            }
        }

        private IEnumerator MoveToObject()
        {
            while (inHommingAttack && targetObject != null)
            {
                rb.MovePosition(Vector3.MoveTowards(transform.position, targetObject.GetComponent<Collider>().bounds.center, hommingAttackSpeed * Time.fixedDeltaTime));
                yield return null;
            }
        }
    }
}
