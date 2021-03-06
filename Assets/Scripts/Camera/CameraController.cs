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

    [SerializeField] List<string> tags = new List<string>();

    private bool canMove;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        CameraControllerManager.AddCameraController(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((cam.gameObject.GetComponent<FixedCamera>() != null) && ((other.gameObject.tag == "Player") || tags.Contains(other.gameObject.tag)))
        {
            CameraControllerManager.DisableCameraControllers();
            canMove = true;
            Debug.Log(CanMove);

            if (changePosition)
            {
                cam.GetComponent<FixedCamera>().SetOffset(newOffset);
            }

            if (changeRotation)
            {
                StartCoroutine(ChangeCameraRotation());
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
            CameraControllerManager.DisableCameraControllers();
            canMove = true;
            Debug.Log(CanMove);

            if (changePosition)
            {
                cam.GetComponent<FixedCamera>().SetOffset(newOffset);
            }

            if (changeRotation)
            {
                StartCoroutine(ChangeCameraRotation());
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
        while (cam.transform.rotation != Quaternion.Euler(newRotation) && canMove)
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
