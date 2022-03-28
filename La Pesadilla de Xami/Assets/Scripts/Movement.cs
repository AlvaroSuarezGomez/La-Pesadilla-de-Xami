using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private Jump jumpScript;
    [SerializeField] private float gravitySpeed = -9.8f;
    [SerializeField] private float rayDistance;
    [SerializeField] private float antigravitySpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    public float GetRayDistance => rayDistance;

    private Vector3 direction;
    private Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpScript = GetComponent<Jump>();
        cam = Camera.main;
        Physics.gravity = gravitySpeed * Vector3.down;
    }

    private void Update()
    {
        Debug.Log(velocity);

        if (((Mathf.Abs(velocity.z) > antigravitySpeed) || (Mathf.Abs(velocity.x) > antigravitySpeed)) && (jumpScript.IsGrounded()))
        {
            rb.useGravity = false;
        } else rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        Accelerate();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0f, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        /*if (vertical != 0f || horizontal != 0f)
        {
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z), 30f * Time.deltaTime));
        }*/

        rb.MovePosition(transform.position + Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, velocity.z * Time.deltaTime));

        if (jumpScript.IsGrounded())
        {
            RaycastHit hit;

            Physics.Raycast(transform.position, -transform.up, out hit, rayDistance, jumpScript.GetJumpLayer());

            Quaternion lookDir = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            Quaternion smoothDir = Quaternion.RotateTowards(transform.rotation, lookDir, 360f * Time.deltaTime);

            rb.MoveRotation(Quaternion.Euler(smoothDir.eulerAngles.x, transform.rotation.eulerAngles.y, smoothDir.eulerAngles.z));

        }
        else
        {
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.eulerAngles.y, 0f), 90f * Time.deltaTime));
            Debug.Log("NotGrounded");
        }
        
    }
    private void Accelerate()
    {
        if ((direction.x != 0f) && (Mathf.Abs(velocity.x) < maxSpeed))
        {
            velocity.x += acceleration * Time.deltaTime * direction.x;
        }
        else if ((direction.x == 0f) && (Mathf.Abs(velocity.x) > 0f))
        {
            if (velocity.x > 0f)
            {
                velocity.x -= acceleration * Time.deltaTime;
            }
            else if (velocity.x < 0f)
            {
                velocity.x += acceleration * Time.deltaTime;
            }
        }
        else
        {
            velocity.x = Mathf.Abs(velocity.x) * direction.x;
        }

        if ((direction.z != 0f) && (Mathf.Abs(velocity.z) < maxSpeed))
        {
            velocity.z += acceleration * Time.deltaTime * direction.z;
        }
        else if ((direction.z == 0f) && (Mathf.Abs(velocity.z) > 0f))
        {
            if (velocity.z > 0f)
            {
                velocity.z -= acceleration * Time.deltaTime;
            }
            else if (velocity.z < 0f)
            {
                velocity.z += acceleration * Time.deltaTime;
            }
        }
        else
        {
            velocity.z = Mathf.Abs(velocity.z) * direction.z;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -transform.up);
    }
}
