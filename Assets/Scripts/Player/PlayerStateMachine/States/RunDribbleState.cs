using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class RunDribbleState : BaseState<PlayerStateMachine.PlayerState>
{
    Player player;
    public RunDribbleState(Player player, PlayerStateMachine.PlayerState key) : base(key) {
        this.player = player;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        var logId = "UpdateState";
        if (player.PlayerInput.ThrowInput)
        {
            var currentThrowBoard = player.PlayerPointer.TargetBoard;
            player.SetThrowBoard(currentThrowBoard);
            if (currentThrowBoard == null)
            {
                Utils.logd(logId, "No target board!");
                return;
            }
            Utils.logd(logId, "Target board=" + currentThrowBoard + " => Throwing basketball");
            player.PlayerStateMachine.StartThrowBasketball();
        }
    }
    public override void ExitState()
    {
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if (player.PlayerMovement.CurrentSpeed < 0.5f) {
            return PlayerStateMachine.PlayerState.IdleDribble;
        }
        return PlayerStateMachine.PlayerState.RunDribble;
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

}