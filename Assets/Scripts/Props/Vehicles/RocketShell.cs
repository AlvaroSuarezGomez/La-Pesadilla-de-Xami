using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private Vector3 velocity;
    private Vector3 localVelocity;
    private RaycastHit ray;
    private bool isGrounded;
    private Vector3 groundNormal;
    private Quaternion slopeRotation;
    private float waitAndRotate;
    private bool isJumping;
    

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
        speed *= 2;
        StartCoroutine(DeactivateDash());
    }

    private IEnumerator DeactivateDash()
    {
        yield return new WaitForSeconds(dashDeactivationTime);
        speed /= 2;
    }

    private void JumpAction_performed(InputAction.CallbackContext obj)
    {
        if (isGrounded && activated)
        {
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
            if (!isJumping)
            {
                GroundCheck();
            }
            Gravity();
            SlopeRotation();
            Move();
            

            //Debug.Log(velocity);
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
        if (!isGrounded)
        {
            //localVelocity.y = Mathf.Lerp(localVelocity.y, -50f, 1f * Time.deltaTime);
            rb.velocity -= transform.up * 50f * Time.deltaTime;
        }

        else
        {
            localVelocity.y = Mathf.Lerp(localVelocity.y, 0f, 10f * Time.deltaTime);
        }
        Debug.Log(isGrounded + " " + isJumping + " " + rb.velocity.y);
    }

    private void GroundCheck()
    {
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
        RaycastHit slopeRay;

        if (isGrounded)
        {
            waitAndRotate = waitRotationTime;
            slopeRay = ray;
            groundNormal = slopeRay.normal;
            Debug.DrawRay(ray.point, slopeRay.normal * 2, Color.blue, 1);
        }

        else if (!isGrounded && isJumping) 
        {
            waitAndRotate = waitRotationTime;
            if (Physics.Raycast(transform.position, transform.up, out slopeRay, upperRayDistance, groundLayer))
            {
                isJumping = false;
            }
            groundNormal = slopeRay.normal;
            Debug.DrawRay(ray.point, slopeRay.normal * 2, Color.blue, 1);
        }

        else if (!isGrounded && !isJumping)
        {
            if (waitAndRotate <= 0f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), slopeRotationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                waitAndRotate -= Time.deltaTime;
            }
        }

        if (isGrounded || isJumping)
        {
            slopeRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, slopeRotation, slopeRotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player" && !activated)
        {
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
