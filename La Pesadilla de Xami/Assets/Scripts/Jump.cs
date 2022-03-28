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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(WaitAndDeactivateJump());
        }

        if (Physics.Raycast(transform.position, -transform.up, GetComponent<Movement>().GetRayDistance, jumpLayer))
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (activateJump)
        {
            rb.AddForce(jumpSpeed * transform.up);
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public LayerMask GetJumpLayer()
    {
        return jumpLayer;
    }

    private IEnumerator WaitAndDeactivateJump()
    {
        activateJump = true;
        yield return new WaitForSeconds(0.1f);
        activateJump = false;
    }
}
