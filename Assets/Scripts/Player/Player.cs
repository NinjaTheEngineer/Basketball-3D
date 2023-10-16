using System;
using System.Collections;
using NinjaTools;
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
    [SerializeField] GameObject playerCamera;
    public float score = 0;
    public float threePointDistance = 5.5f;
    public float distanceFromThrow;
    public static Action<Player, int> OnPlayerScore;
    [SerializeField] float meterDisappearDelay = 1f;
    Board _lastThrowBoard;
    public float interactableRadius = 2f;
    public Board LastThrowBoard {
        get => _lastThrowBoard;
        private set {
            var logId = "CurrentBoard_set";
            if(_lastThrowBoard) {
                _lastThrowBoard.OnScore-=AddScore;
            }
            _lastThrowBoard = value;
            if(_lastThrowBoard) {
                _lastThrowBoard.OnScore+=AddScore;
            }
        }
    }
    public void SetThrowBoard(Board throwBoard) => LastThrowBoard = throwBoard;
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
    void AddScore() {
        distanceFromThrow = Vector3.Distance(transform.position, _lastThrowBoard.ThrowTarget.position);
        int throwScore = 2;
        if(distanceFromThrow >= threePointDistance) {
            throwScore = 3;
        }
        OnPlayerScore?.Invoke(this, throwScore);
        logd("AddScore", "Player Scored!");
    }
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
    public InteractableObject NearestInteractable { 
        get {
            Collider[] colliders = Physics.OverlapSphere(ballPickupPos.position, interactableRadius);
            var collidersCount = colliders.Length;
            for (int i = 0; i < collidersCount; i++) {
                InteractableObject interactableObject = colliders[i].GetComponent<InteractableObject>();
                if (interactableObject != null) {
                    return interactableObject;
                }
            }
            return null;
        } 
    }

}
