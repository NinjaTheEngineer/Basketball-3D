using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class IdleState : BaseState<PlayerStateMachine.PlayerState>
{
    Player player;
    public IdleState(Player player, PlayerStateMachine.PlayerState key) : base(key) {
        if(player==null) {
            return;
        }
        this.player = player;
    }

    public override void EnterState() {
    }

    public override void UpdateState() {
        if (player.PlayerInput.InteractInput && player.CurrentBasketball == null) {
            var nearestInteractable = player.NearestInteractable; 
            nearestInteractable?.OnInteract(player.gameObject);
        }
    }
    public override void ExitState() {
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if (player.PlayerMovement.CurrentSpeed > 0.5f) {
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

}