using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShell : Vehicle
{

    [SerializeField] private float groundRayDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float slopeRotationSpeed;

    private RaycastHit ray;
    private bool isGrounded;
    private Vector3 groundNormal;
    private Quaternion slopeRotation;

    private void FixedUpdate()
    {
        if (activated)
        {
            GroundCheck();
            SlopeRotation();

            rb.velocity = new Vector3(0f, rb.velocity.y, 0f) + transform.forward * speed;
            Vector2 dir = rotateAction.ReadValue<Vector2>();
            transform.Rotate(new Vector3(0, dir.x, 0) * rotationSpeed * Time.fixedDeltaTime);
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
            slopeRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation, slopeRotationSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(slopeRotation);
        }
        else
        {

            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), slopeRotationSpeed * Time.fixedDeltaTime));
        }
    }
}
