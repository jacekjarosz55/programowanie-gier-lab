using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{

    public List<Camera> cameras = new();
    public InputActionProperty switchAction;

    private int activeCameraIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameras.ForEach(x => x.enabled = false);
        cameras[activeCameraIndex].enabled = true;
        switchAction.action.performed += SwitchAction_Performed;
    }

    private void SwitchAction_Performed(InputAction.CallbackContext context)
    {
        cameras[activeCameraIndex].enabled = false;
        activeCameraIndex = (activeCameraIndex + 1) % cameras.Count;
        cameras[activeCameraIndex].enabled = true;
    }
}
