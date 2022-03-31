using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private Jump jumpScript;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    private Vector3 direction;
    private Vector3 velocity;
    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpScript = GetComponent<Jump>();
        cam = Camera.main;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        velocity.x = Accelerate(direction.x, velocity.x);
        velocity.z = Accelerate(direction.z, velocity.z);

        HandleMovement();
        Crash();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0f, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        if (vertical != 0f || horizontal != 0f)
        {
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z), 30f * Time.deltaTime);
        }



        rb.velocity = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(velocity.x, velocity.y, velocity.z);


        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.eulerAngles.y, 0f), 90f * Time.deltaTime));
    }

    private float Accelerate(float d, float v)
    {
        if ((d != 0f) && (Mathf.Abs(v) < maxSpeed))
        {
            v += acceleration * Time.deltaTime * d;
        }
        else if ((d == 0f) && (Mathf.Abs(v) > 0f))
        {
            if (v > 0f)
            {
                v -= acceleration * Time.deltaTime;
            }
            else if (velocity.x < 0f)
            {
                v += acceleration * Time.deltaTime;
            }
        }
        else
        {
            v = Mathf.Abs(v) * d;
        }

        return v;
    }

    private void Crash()
    {
        if ((Physics.Raycast(transform.position, direction, .2f)))
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -transform.up);
        Gizmos.DrawLine(transform.position, direction);
    }
}
