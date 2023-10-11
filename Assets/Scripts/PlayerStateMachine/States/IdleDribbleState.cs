using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class IdleDribbleState : BaseState<PlayerStateMachine.PlayerState> {
    PlayerStateMachine playerStateMachine;
    public IdleDribbleState(PlayerStateMachine stateMachine, PlayerStateMachine.PlayerState key) : base(stateMachine, key) {
        playerStateMachine = stateMachine;
    }

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if(playerStateMachine.CurrentSpeed > 1) {
            return PlayerStateMachine.PlayerState.Run;
        }
        return PlayerStateMachine.PlayerState.Idle;
    }

    public override void OnTriggerEnter(Collider other) {
    }

    public override void OnTriggerExit(Collider other) {
    }

    public override void OnTriggerStay(Collider other) {
    }

    public override void UpdateState() {
    }
}
