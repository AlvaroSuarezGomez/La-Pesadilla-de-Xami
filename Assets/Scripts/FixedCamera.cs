using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 offset;
    [SerializeField] public bool lookAtObject;
    [SerializeField] public Transform target;

    private void Update()
    {
        transform.position = parent.position - (transform.rotation * offset);
        PlayerRotationCamera();
        if (lookAtObject)
        {
            RotationRelativeToObject();
        }
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
        //transform.rotation = Quaternion.Euler(parent.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, parent.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void RotationRelativeToObject()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 90f);
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
