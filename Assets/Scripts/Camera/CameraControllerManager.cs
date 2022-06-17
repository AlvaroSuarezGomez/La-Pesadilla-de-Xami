using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerManager : MonoBehaviour
{
    private static List<CameraController> cameraControllers = new List<CameraController>();
    public static List<CameraController> CameraControllers => cameraControllers;


    public static void DisableCameraControllers()
    {
        foreach (CameraController cc in cameraControllers) {
            cc.CanMove = false;
        }
    }

    public static void AddCameraController(CameraController cc)
    {
        cameraControllers.Add(cc);
    }
}
