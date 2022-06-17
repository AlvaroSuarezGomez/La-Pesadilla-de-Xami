using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] private Transform parent;
    public Transform Parent { get { return parent; } set { parent = value; } }

    [SerializeField] private Vector3 offset;
    public bool lookAtObject; 
    public bool lookAtParent;
    [SerializeField] public Transform target;
    [SerializeField] private float smoothRotationTime;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, parent.position - (transform.rotation * offset), 100f);
        if (lookAtObject)
        {
            RotationRelativeToObject();
        }

        if (lookAtParent)
        {
            ParentDirection();
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
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 100f);
    }

    public void ParentDirection()
    {
        transform.position = Vector3.MoveTowards(transform.position, parent.transform.position + (parent.transform.rotation * -offset), smoothRotationTime * Time.deltaTime);
        Quaternion rotationY = Quaternion.Euler(0f, parent.rotation.eulerAngles.y, 0f);
        Quaternion rotationXZ = Quaternion.Slerp(transform.rotation, parent.rotation, smoothRotationTime * Time.deltaTime);

        transform.rotation = Quaternion.Euler(rotationXZ.eulerAngles.x, rotationY.eulerAngles.y, rotationXZ.eulerAngles.z);

        //transform.LookAt(parent);
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }

    public void SetParent(Transform newParent)
    {
        parent = newParent;
    }
}
