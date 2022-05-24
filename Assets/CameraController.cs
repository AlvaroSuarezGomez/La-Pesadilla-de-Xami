using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 newOffset;
    [SerializeField] private Vector3 newRotation;
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool changePosition;
    [SerializeField] private bool changeRotation;
    [SerializeField] private bool changeAnchorPoint;

    private static Coroutine cameraChangerCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if ((cam.gameObject.GetComponent<FixedCamera>() != null) && (other.gameObject.tag == "Player"))
        {
            if (cameraChangerCoroutine != null)
            {
                StopCoroutine(cameraChangerCoroutine);
            }

            if (changePosition)
            {
                cam.GetComponent<FixedCamera>().SetOffset(newOffset);
            }

            if (changeRotation)
            {
                cameraChangerCoroutine = StartCoroutine(ChangeCameraRotation());
            }

            if (anchorPoint != null)
            {
                cam.GetComponent<FixedCamera>().target = anchorPoint;
            }

            cam.GetComponent<FixedCamera>().lookAtObject = changeAnchorPoint;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((cam.gameObject.GetComponent<FixedCamera>() != null) && (collision.gameObject.tag == "Player"))
        {
            if (cameraChangerCoroutine != null)
            {
                StopCoroutine(cameraChangerCoroutine);
            }

            if (changePosition)
            {
                cam.GetComponent<FixedCamera>().SetOffset(newOffset);
            }

            if (changeRotation)
            {
                cameraChangerCoroutine = StartCoroutine(ChangeCameraRotation());
            }

            if (anchorPoint != null)
            {
                cam.GetComponent<FixedCamera>().target = anchorPoint;
            }

            cam.GetComponent<FixedCamera>().lookAtObject = changeAnchorPoint;
        }
    }

    private IEnumerator ChangeCameraRotation()
    {
        while (cam.transform.rotation != Quaternion.Euler(newRotation))
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
