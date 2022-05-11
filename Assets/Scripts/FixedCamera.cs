using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool lookAtObject;
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = parent.position - (parent.rotation * offset);
        PlayerRotationCamera();
        //DetectCollision();
    }

    private void DetectCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, offset.z - 2))
        {
            offset.z = offset.z - hit.distance;
            Debug.Log("Colisiona");
        } else
        {
            offset.z = 8f;
        }
    }

    private void PlayerRotationCamera()
    {
        transform.rotation = Quaternion.Euler(parent.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, parent.rotation.eulerAngles.z);
    }
}
