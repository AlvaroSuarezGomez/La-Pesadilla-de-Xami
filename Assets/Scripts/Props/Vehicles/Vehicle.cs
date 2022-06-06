using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Player;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    protected bool activated;
    [SerializeField]
    public bool Activated { get { return activated; } set { activated = value; } }
    [SerializeField]
    protected Rigidbody rb;
    [SerializeField]
    protected float speed = 10;
    [SerializeField]
    protected float rotationSpeed = 10;
    [SerializeField]
    protected InputActionAsset input;
    [SerializeField]
    protected Transform attachPoint;
    [SerializeField]
    protected GameObject player;
    protected InputAction rotateAction;

    protected virtual void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        input.Enable();

        rotateAction = input.FindAction("Move");

    }

    protected virtual void Update()
    {
        if (player != null)
        {
            player.transform.position = attachPoint.position;
            player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            other.GetComponent<Movement>().CanMove = false;
            other.transform.parent = attachPoint;
            activated = true;
        }
    }
}
