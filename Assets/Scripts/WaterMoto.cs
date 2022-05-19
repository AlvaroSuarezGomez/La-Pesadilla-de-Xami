using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterMoto : MonoBehaviour
{
    [SerializeField]
    private bool activated;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private InputActionAsset input;
    private InputAction rotateAction;

    private void Awake()
    {
        input.Enable();

        rotateAction = input.FindAction("Move");

    }



    private void FixedUpdate()
    {
        if (activated)
        {
            rb.velocity = transform.forward*speed;
            Vector2 dir = rotateAction.ReadValue<Vector2>();
            transform.Rotate(new Vector3(0,dir.x,0)*rotationSpeed*Time.fixedDeltaTime);
        }
    }
}
