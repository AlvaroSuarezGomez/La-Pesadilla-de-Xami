using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Xami.Player;

namespace Xami.Vehicles
{
    public class RocketShell : Vehicle
    {

        [SerializeField] private float groundRayDistance;
        [SerializeField] private float upperRayDistance;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float slopeRotationSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float waitRotationTime;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private InputActionReference dashAction;
        [SerializeField] private float dashDeactivationTime;
        [SerializeField] private Animator anim;
        [SerializeField] private float animationTime;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource levelMusic;
        [SerializeField] private AudioClip music;
        [SerializeField] private float jumpTime;

        private Vector3 rayDir;
        private Vector3 velocity;
        private Vector3 localVelocity;
        private RaycastHit ray;
        private bool isGrounded;
        private Vector3 groundNormal;
        private bool rotateToGround = true;
        private Quaternion slopeRotation;
        private float waitAndRotate;
        private bool isJumping;
        public bool IsJumping => isJumping;
        private bool isDashing;

        [SerializeField] private bool followCamera;


        protected override void Awake()
        {
            base.Awake();
            jumpAction.action.performed += JumpAction_performed;
            dashAction.action.performed += DashAction_performed;

            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        private void DashAction_performed(InputAction.CallbackContext obj)
        {
            if (!isDashing) {
                speed *= 2;
                isDashing = true;
                StartCoroutine(DeactivateDash());
            }
        }

        private IEnumerator DeactivateDash()
        {
            yield return new WaitForSeconds(dashDeactivationTime);
            isDashing = false;
            speed /= 2;
        }

        private void JumpAction_performed(InputAction.CallbackContext obj)
        {
            if (isGrounded && activated)
            {
                if (Physics.Raycast(transform.position, transform.up, out ray, upperRayDistance, groundLayer))
                {
                    Debug.Log(ray.transform.gameObject);
                    rotateToGround = false;
                }

                rb.velocity += transform.up * jumpForce;
                isJumping = true;
                isGrounded = false;

                StopCoroutine(WaitAndDisableJump());
                StartCoroutine(WaitAndDisableJump());
            }
        }

        private void FixedUpdate()
        {
            if (activated)
            {
                if (rotateToGround)
                {
                    GroundCheck();
                }
                Gravity();
                SlopeRotation();
                Move();


               //Debug.Log(rb.velocity);
               //Debug.Log("isGrounded: " + isGrounded + "; rotateToGround: " + rotateToGround);
            }
            //Debug.Log(isGrounded + " " + velocity);
            //Debug.Log(localVelocity.y);
        }

        private void Move()
        {
            velocity = rb.velocity;
            localVelocity = transform.InverseTransformDirection(velocity);
            localVelocity = new Vector3(0f, localVelocity.y, speed);
            velocity = transform.TransformDirection(localVelocity);
            rb.velocity = velocity;

            Vector2 dir = rotateAction.ReadValue<Vector2>();
            transform.Rotate(0f, dir.x * rotationSpeed * Time.fixedDeltaTime, 0f);
        }

        private void Gravity()
        {
            if (!isGrounded && !isJumping)
            {
                //localVelocity.y = Mathf.Lerp(localVelocity.y, -50f, 1f * Time.deltaTime);
                rb.velocity -= transform.up * 50f * Time.deltaTime;
            }

            else
            {
                localVelocity.y = Mathf.Lerp(localVelocity.y, 0f, 10f);
            }
            //Debug.Log(isGrounded + " " + isJumping + " " + rb.velocity.y);a
        }

        private void GroundCheck()
        {
            Debug.DrawRay(transform.position, transform.up * upperRayDistance, Color.red, 1);
            Debug.DrawRay(transform.position, -transform.up * groundRayDistance, Color.red, 1);
            if (Physics.Raycast(transform.position, -transform.up, out ray, groundRayDistance, groundLayer))
            {
                //Debug.Log("YES ground");
                isGrounded = true;
            }
            else
            {
                //Debug.Log("NO ground");
                isGrounded = false;
            }
        }

        private void SlopeRotation()
        {
            if (isGrounded || !rotateToGround)
            {
                if (isGrounded || isJumping)
                {
                    waitAndRotate = waitRotationTime;
                }
                groundNormal = ray.normal;
                
                slopeRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

                if (rotateToGround)
                {
                    transform.rotation = slopeRotation;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z);
                    rotateToGround = true;
                }
                //rb.MoveRotation(slopeRotation);
                    Debug.Log(groundNormal);
                    

                Debug.DrawRay(ray.point, ray.normal * 2, Color.blue, 1);
            }

            else if (!isGrounded && !isJumping)
            {
                if (waitAndRotate <= 0f)
                {
                    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), slopeRotationSpeed * Time.deltaTime));
                }
                else
                {
                    waitAndRotate -= Time.deltaTime;
                }
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.tag == "Player" && !activated)
            {
                //player.gameObject.GetComponent<Collider>().enabled = false;
                player.GetComponent<Movement>().PlayerModel.rotation = Quaternion.Euler(Vector3.zero);
                player.GetComponent<Movement>().ridingShell = true;
                if (followCamera)
                {
                    cam.Parent = transform;
                    cam.lookAtParent = true;
                } else
                {
                    cam.SetParent(transform); 
                    cam.SetOffset(new Vector3(0, -2f, 10f));
                }
                
                anim.SetBool("Activated", true);
                audioSource.Play();
                StartCoroutine(WaitAndActivate());
            }
        }

        private IEnumerator WaitAndActivate()
        {
            yield return new WaitForSeconds(animationTime);
            activated = true;
            if (music != null)
            {
                levelMusic.clip = music;
                levelMusic.Play();
            }
        }

        private IEnumerator WaitAndDisableJump()
        {
            yield return new WaitForSeconds(jumpTime);
            isJumping = false;
        }
    }
}
