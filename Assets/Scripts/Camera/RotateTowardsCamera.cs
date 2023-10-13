using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class RotateTowardsCamera : NinjaMonoBehaviour {
    Camera mainCamera;
    void Start() {
        var logId = "Start";
        mainCamera = Camera.main;

        if (mainCamera == null) {
            loge(logId, "Main camera not found. Make sure you have a camera in the scene.");
        }
    }

    private void Update() {
        if (mainCamera == null) {
            return;
        }
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}