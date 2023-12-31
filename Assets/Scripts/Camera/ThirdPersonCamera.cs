using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class ThirdPersonCamera : NinjaMonoBehaviour {
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform visu;
    public Rigidbody rb;

    public float rotationSpeed;
    void Update() {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero) {
            visu.forward = Vector3.Slerp(visu.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
