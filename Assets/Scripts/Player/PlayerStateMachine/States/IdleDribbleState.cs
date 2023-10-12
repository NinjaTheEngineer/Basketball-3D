using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class IdleDribbleState : BaseState<PlayerStateMachine.PlayerState> {
    Player player;
    public IdleDribbleState(Player player, PlayerStateMachine.PlayerState key) : base(key) {
        this.player = player;
    }

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if(player.PlayerMovement.CurrentSpeed > 0.5f) {
            return PlayerStateMachine.PlayerState.RunDribble;
        }
        return PlayerStateMachine.PlayerState.IdleDribble;
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
