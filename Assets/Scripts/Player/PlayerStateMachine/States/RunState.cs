using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class RunState : BaseState<PlayerStateMachine.PlayerState> {
    Player player;
    public RunState(Player player, PlayerStateMachine.PlayerState key) : base(key) {
        this.player = player;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if (player.PlayerMovement.CurrentSpeed < 0.5f) {
            return PlayerStateMachine.PlayerState.Idle;
        }
        return PlayerStateMachine.PlayerState.Run;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void UpdateState()
    {
    }
}