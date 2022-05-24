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
        }
        else
        {
            offset.z = 8f;
        }
    }

    public void RotationRelativeToObject()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
