using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private float jumpSpeed = 5f;
    private bool activateJump;
    private bool isGrounded;
    private Rigidbody rb;
    [SerializeField] private float rayDistance;
    private RaycastHit slopeHit;
    [SerializeField] private float antigravitySpeed;
    private Movement movementScript;
    [SerializeField] private float gravitySpeed = 9.8f;

    public bool IsGrounded => isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(WaitAndDeactivateJump());
        }
    }

    

    private void FixedUpdate()
    {
        if (activateJump)
        {
            movementScript.Velocity += new Vector3(0f, jumpSpeed, 0f);
        }

        Gravity();
        GroundDetection();
        SlopeRotation();
    }

    private void Gravity()
    {
        if (((Mathf.Abs(movementScript.Velocity.z) > antigravitySpeed) || (Mathf.Abs(movementScript.Velocity.x) > antigravitySpeed)) && (isGrounded))
        {
            movementScript.Velocity = Vector3.zero;
        }
        else
        {
            movementScript.Velocity -= new Vector3(0f, gravitySpeed, 0f);
        }
    }

    private void GroundDetection()
    {
        if (Physics.Raycast(transform.position, -transform.up, out slopeHit, rayDistance, jumpLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void SlopeRotation()
    {
        if (isGrounded)
        {
            Quaternion lookDir = Quaternion.FromToRotation(transform.up, slopeHit.normal) * transform.rotation;
            Quaternion smoothDir = Quaternion.RotateTowards(transform.rotation, lookDir, 360f * Time.deltaTime);

            rb.MoveRotation(Quaternion.Euler(smoothDir.eulerAngles.x, transform.rotation.eulerAngles.y, smoothDir.eulerAngles.z));

        }
    }

    private IEnumerator WaitAndDeactivateJump()
    {
        activateJump = true;
        yield return new WaitForSeconds(.1f);
        activateJump = false;
    }
}
