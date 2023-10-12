using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;

public class Basketball : InteractableObject {

    [SerializeField] GameObject visu;
    Rigidbody rb;
    public enum BasketballState {
        Free,
        BeingPickedUp,
        PickedUp,
        Thrown
    }
    public BasketballState CurrentState { get; private set; }
    public override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        CurrentState = BasketballState.Free;
    }

    public override void OnInteract(GameObject interactor) {
        var logId = "OnInteract";
        var player = interactor.GetComponent<Player>();
        if(player==null) {
            logw(logId, "Player not found => no-op");
            return;
        }
        logd(logId, "Player="+player+" => Picking up ball");
        player.PickUpBall(this);
        nearIndicator?.Hide();
        CurrentState = BasketballState.BeingPickedUp;
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }
    public void OnPickedUp() {
        CurrentState = BasketballState.PickedUp;
        rb.isKinematic = true;
        visu.SetActive(false);
    }

    public override void OnTriggerEnter(Collider other) {
        var logId = "OnTriggerEnter";
        if (other.tag!="Player") {
            return;
        }
        if (CurrentState == BasketballState.Free) {
            nearIndicator?.Show(transform);
        }
        logd(logId, "Other=" + other.logf());
    }

    public override void OnTriggerExit(Collider other) {
        var logId = "OnTriggerExit";
        if (other.tag!="Player") {
            return;
        }
        nearIndicator?.Hide();
        logd(logId, "Other=" + other.logf());
    }
    public float throwForce = 2f;
    public void Throw(Vector3 throwDirection) {
        CurrentState = BasketballState.Thrown;
        nearIndicator?.Hide();
        visu.SetActive(true);
        initialPos = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(initialPos, targetPos);
        throwSpeed = journeyLength / 2f;
    }
    public Vector3 targetPos;
    Vector3 initialPos;
    float journeyLength;
    float startTime;
    public float throwSpeed = 10f;
    bool throwing = false;
    public float gravity = 9.81f; // Standard gravity value
    void Update() {
        if(throwing) {
            float journeyDuration = Time.time - startTime;
            var distance = Vector3.Distance(transform.position, targetPos);
            if (distance > 0.25f) {
                // Calculate the normalized distance covered
                float distanceCovered = journeyDuration * throwSpeed;

                // Calculate the fraction of the journey completed
                float fractionOfJourney = distanceCovered / journeyLength;

                // Use the Lerp function to calculate the position of the basketball
                Vector3 newPosition = Vector3.Lerp(initialPos, targetPos, fractionOfJourney);

                // Apply gravity to create an arcing motion
                float yOffset = Mathf.Sin(fractionOfJourney * Mathf.PI) * 2.0f; // Adjust amplitude as needed
                newPosition.y += yOffset;

                // Update the position of the basketball
                transform.position = newPosition;
            } else {
                throwing = false;
                CurrentState = BasketballState.Free;
                nearIndicator?.Hide();
                rb.isKinematic = false;
            }
        }
    }
}
