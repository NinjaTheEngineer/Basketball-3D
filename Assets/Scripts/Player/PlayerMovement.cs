using System;
using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class PlayerMovement : NinjaMonoBehaviour {
    Player player;
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
    Vector3 currentVelocity;
    public float CurrentSpeed => currentVelocity.magnitude;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        player = GetComponent<Player>();
    }

    void FixedUpdate() {
        if (rb.isKinematic) {
            return;
        }
        MovePlayer();
    }
    private void LateUpdate() {
        currentVelocity = rb.velocity;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(orientation.position, orientation.position  + (Camera.main.transform.forward * 100));
    }
    private void MovePlayer() {
        var logId = "MovePlayer";
        var verticalInput = player.PlayerInput.VerticalInput;
        var horizontalInput = player.PlayerInput.HorizontalInput;

        if (verticalInput == 0 && horizontalInput == 0) {
            return;
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        var force = moveDirection.normalized * moveSpeed * 10f;
        rb.AddForce(force, ForceMode.Force);
        logt(logId, "MoveDirection=" + moveDirection + " Force=" + force);
    }

    public void SetKinematic(bool isKinematic) {
        var logId = "SetKinematic";
        if(isKinematic) {
            rb.velocity = Vector3.zero;
        }
        logd(logId, "Setting Rigidbody to Kinematic="+isKinematic);
        rb.isKinematic = isKinematic;
    }
}
