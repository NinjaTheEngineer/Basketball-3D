using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 2.0f;

    private float rotationX = 0.0f; // Current camera rotation around the X-axis

    void Update()
    {
        // Get mouse input for horizontal rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Rotate the camera horizontally based on mouse movement
        rotationX += mouseX;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); // Clamp the vertical rotation to limit it within a reasonable range

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
    }
}