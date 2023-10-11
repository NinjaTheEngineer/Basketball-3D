using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState> {
    public enum PlayerState
    {
        Idle,
        IdleDribble,
        Run,
        RunDribble,
        Throw,
        PickUp
    }
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    [SerializeField] Vector3 Velocity;
    public float CurrentSpeed => Velocity.magnitude;
    public Animator anim;
    private void Awake() {
        SetStates();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }
    void SetStates() {
        States.Add(PlayerState.Idle, new IdleState(this, PlayerState.Idle));
        States.Add(PlayerState.IdleDribble, new IdleDribbleState(this, PlayerState.IdleDribble));
        States.Add(PlayerState.Run, new RunState(this, PlayerState.Run));
        States.Add(PlayerState.RunDribble, new RunDribbleState(this, PlayerState.RunDribble));
        States.Add(PlayerState.Throw, new ThrowState(this, PlayerState.Throw));
        States.Add(PlayerState.PickUp, new PickUpState(this, PlayerState.PickUp));
        CurrentState = States[PlayerState.Idle];
    }

    public override void Update() {
        var logId = "Update";
        base.Update();
        HandleInput();
    }

    void FixedUpdate() {
        if (CurrentState==States[PlayerState.Throw]) {
            rb.velocity = Vector3.zero;
            return;
        }
        MovePlayer();
    }
    private void LateUpdate() {
        Velocity = rb.velocity;
    }
    private void HandleInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer() {
        if(verticalInput==0 && horizontalInput==0) {
            return;
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        var force = moveDirection.normalized * moveSpeed * 10f;
        rb.AddForce(force, ForceMode.Force);
        logd("LogId", "MoveDirection="+moveDirection+ " Force="+ force);
    }

    public override void OnStateChange(PlayerState state) {
        anim.Play(state.ToString());
    }
}
