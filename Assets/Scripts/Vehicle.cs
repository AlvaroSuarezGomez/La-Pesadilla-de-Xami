using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    protected bool activated;
    [SerializeField]
    protected Rigidbody rb;
    [SerializeField]
    protected float speed = 10;
    [SerializeField]
    protected float rotationSpeed = 10;
    [SerializeField]
    protected InputActionAsset input;
    protected InputAction rotateAction;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        input.Enable();

        rotateAction = input.FindAction("Move");

    }
}
