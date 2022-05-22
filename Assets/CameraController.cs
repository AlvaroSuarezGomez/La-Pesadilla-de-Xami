using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 newOffset;
    [SerializeField] private Vector3 newRotation;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool changePosition;
    [SerializeField] private bool changeRotation;

    private void OnTriggerExit(Collider other)
    {
        if (cam.gameObject.GetComponent<FixedCamera>() != null)
        {
            if (changePosition)
            {
                cam.GetComponent<FixedCamera>().SetOffset(newOffset);
            }

            if (changeRotation)
            {
                StartCoroutine(ChangeCameraRotation());
            }
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
