using System;
using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : NinjaMonoBehaviour {
    [field: SerializeField] public PlayerStateMachine PlayerStateMachine { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerThrower PlayerThrower { get; private set; }
    [field: SerializeField] public PlayerPointer PlayerPointer { get; private set; }
    [field: SerializeField] public PowerMeter PowerMeter { get; private set;}
    [field: SerializeField] public Basketball CurrentBasketball { get; private set; }
    public BasketballHolder basketballHolder;
    public Transform ballPickupPos;
    public Animator anim;
    public float score = 0;
    public float threePointDistance = 5.5f;
    private void Awake() {
        PlayerStateMachine = GetComponent<PlayerStateMachine>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerThrower = GetComponent<PlayerThrower>();
        PlayerPointer = GetComponent<PlayerPointer>();
        anim = GetComponentInChildren<Animator>();
    }
    public void ThrowBasketball() {
        var logId = "ThrowBasketball";
        logd(logId, "Starting to physically throw basketball!");
        basketballHolder.HideBasketball();
        PlayerThrower.ThrowBasketball(CurrentBasketball, LastThrowBoard);
        CurrentBasketball = null;
        HideMeter();
    }
    public float distanceFromThrow;
    void AddScore() {
        distanceFromThrow = Vector3.Distance(transform.position, _lastThrowBoard.ThrowTarget.position);
        if(distanceFromThrow >= threePointDistance) {
            score += 3;
        } else {
            score += 2;
        }
        logd("AddScore", "Player Scored!");
    }
    [SerializeField] float meterDisappearDelay = 1f;
    IEnumerator HideMeterRoutine() {
        yield return new WaitForSeconds(meterDisappearDelay);
        PowerMeter.gameObject.SetActive(false);
    }
    public void HideMeter() => StartCoroutine(HideMeterRoutine());
    public void ShowMeterRoutine() {
        PowerMeter.gameObject.SetActive(true);
    }
    public void PickUpBall(Basketball basketball) {
        var logId = "PickUpBall";
        if (basketball == null) {
            logw(logId, "Basketball is null => no-op");
            return;
        }
        basketball.transform.position = ballPickupPos.position;
        var ballPos = basketball.transform.position;
        var ballDir = ballPos - transform.position;
        CurrentBasketball = basketball;
        logd(logId, "Picking up basketball=" + basketball.logf());
        ballDir.y = 0;
        transform.forward = ballDir;
        PlayerStateMachine.TransitionToState(PlayerStateMachine.PlayerState.PickUp);
    }

    public void OnPickUpBasketball() {
        var logId = "OnPickUpBasketball";
        if (CurrentBasketball == null) {
            return;
        }
        logd(logId, "Picked Basketball=" + CurrentBasketball.logf());
        basketballHolder.ShowBasketball();
        CurrentBasketball.OnPickedUp(basketballHolder);
    }
    Board _lastThrowBoard;
    public Board LastThrowBoard {
        get => _lastThrowBoard;
        private set {
            var logId = "CurrentBoard_set";
            if(_lastThrowBoard) {
                _lastThrowBoard.OnScore-=AddScore;
            }
            _lastThrowBoard = value;
            _lastThrowBoard.OnScore+=AddScore;
        }
    }
    public void SetThrowBoard(Board throwBoard) => LastThrowBoard = throwBoard;
}
