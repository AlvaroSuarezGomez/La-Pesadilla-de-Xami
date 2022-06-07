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
    [SerializeField] private AudioSource music;

    private Vector3 velocity;
    private Vector3 localVelocity;
    private RaycastHit ray;
    private float gravity;
    private bool isGrounded;
    private bool rotateToGround = true;
    private Vector3 groundNormal;
    private Quaternion slopeRotation;
    private float waitAndRotate;
    

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
            if (Physics.Raycast(transform.position, transform.up, out ray, upperRayDistance, groundLayer))
            {
                rotateToGround = false;
            }
            rb.velocity += transform.up * jumpForce;
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

            SlopeRotation();

            velocity = rb.velocity;
            localVelocity = transform.InverseTransformDirection(velocity);
            localVelocity = new Vector3(0f, localVelocity.y, speed);
            velocity = transform.TransformDirection(localVelocity);
            rb.velocity = velocity;

            Vector2 dir = rotateAction.ReadValue<Vector2>();
            transform.Rotate(0f, dir.x * rotationSpeed * Time.fixedDeltaTime, 0f);

            Debug.Log(velocity);
        }
        Debug.Log(isGrounded + " " + velocity);
        Debug.Log(localVelocity.y);
    }

    protected override void Update()
    {
        base.Update();
        if (!isGrounded)
        {
            if (localVelocity.y > -9.8f)
            {
                localVelocity.y = Mathf.Lerp(gravity, -9.8f, 100f * Time.deltaTime);
            }
        }
        else
        {
            if (localVelocity.y != 0f)
            {
                localVelocity.y = Mathf.Lerp(gravity, 0f, 10f * Time.deltaTime);
            }
        }
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
        if (isGrounded || !rotateToGround)
        {
            if (isGrounded)
            {
                waitAndRotate = 0.5f;
            }

            groundNormal = ray.normal;
            Debug.DrawRay(ray.point, ray.normal * 2, Color.blue, 1);
            slopeRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, slopeRotation, slopeRotationSpeed * Time.fixedDeltaTime);

            if ((transform.rotation == slopeRotation) && !rotateToGround)
            {
                rotateToGround = true;
            }
        }
        else if (!isGrounded)
        {
            if (waitAndRotate <= 0f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), slopeRotationSpeed * Time.fixedDeltaTime);
            } else
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
            anim.SetBool("Activated", true);
            audioSource.Play();
            StartCoroutine(WaitAndActivate());
        }
    }

    private IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(animationTime);
        activated = true;
        music.Play();
    }
}
