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
        rb.velocity = Vector3.zero;
    }
    void Update() {
        if(CurrentState==BasketballState.PickedUp && basketballHolder) {
            transform.position = basketballHolder.transform.position;
        }    
    }
    BasketballHolder basketballHolder;
    public void OnPickedUp(BasketballHolder holder) {
        CurrentState = BasketballState.PickedUp;
        rb.isKinematic = true;
        visu.SetActive(false);
        basketballHolder = holder;
    }

    public override void OnTriggerEnter(Collider other) {
        var logId = "OnTriggerEnter";
        if (CurrentState == BasketballState.PickedUp || other.tag!="Player") {
            return;
        }
        if (CurrentState == BasketballState.Free) {
            nearIndicator?.Show(transform);
        }
        logd(logId, "Player=" + other.logf());
    }

    public override void OnTriggerExit(Collider other) {
        var logId = "OnTriggerExit";
        if (CurrentState == BasketballState.PickedUp || other.tag!="Player") {
            return;
        }
        nearIndicator?.Hide();
        logd(logId, "Player=" + other.logf());
    }
    public void Throw() {
        var logId = "Throw";
        CurrentState = BasketballState.Thrown;
        nearIndicator?.Hide();
        rb.isKinematic = true;
        logd(logId, "Setting Visu to True! IsKinematic=true");
        visu.SetActive(true);
    }
    public void OnThrowEnd(Vector3 endVelocity) {
        var logId = "OnThrowEnd";
        logd(logId, "Throw Ended!");
        rb.isKinematic = false;
        rb.velocity = endVelocity;
        CurrentState = BasketballState.Free;
    }
}
