using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShell : Vehicle
{

    [SerializeField] private float groundRayDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float slopeRotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private InputActionReference jumpAction;

    private RaycastHit ray;
    private bool isGrounded;
    private Vector3 groundNormal;
    private Quaternion slopeRotation;

    protected override void Awake()
    {
        base.Awake();
        jumpAction.action.performed += JumpAction_performed;
    }

    private void JumpAction_performed(InputAction.CallbackContext obj)
    {
        rb.velocity += transform.up * jumpForce;
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            GroundCheck();
            SlopeRotation();

            rb.velocity = new Vector3(transform.forward.x * speed, 0f, transform.forward.z * speed) ;

            Vector2 dir = rotateAction.ReadValue<Vector2>();
            transform.Rotate(new Vector3(0, dir.x * rotationSpeed * Time.fixedDeltaTime, 0f));
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
        if (isGrounded)
        {
            groundNormal = ray.normal;
            Debug.DrawRay(ray.point, ray.normal * 2, Color.blue, 1);
            slopeRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, slopeRotation, slopeRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), slopeRotationSpeed * Time.fixedDeltaTime);
        }
    }
}
