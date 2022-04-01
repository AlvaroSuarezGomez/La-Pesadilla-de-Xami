using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;

    [Header("Ground Physics")]
    private bool isGrounded;

    public bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }
    private RaycastHit ray;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 groundNormal;
    private Quaternion slopeRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        SlopeRotation();
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, out ray, rayDistance, groundLayer))
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
    }

    private void SlopeRotation()
    {
        if (isGrounded)
        {
            groundNormal = ray.normal;
            slopeRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;

            transform.rotation = Quaternion.Euler(slopeRotation.eulerAngles.x, transform.rotation.eulerAngles.y, slopeRotation.eulerAngles.z);
        } else
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }
}
