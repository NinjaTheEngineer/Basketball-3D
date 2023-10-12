using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using UnityEngine.Playables;

public class PickUpState : BaseState<PlayerStateMachine.PlayerState> {
    Player player;   
    public PickUpState(Player player, PlayerStateMachine.PlayerState key) : base(key) {
        this.player = player;
    }

    public override void EnterState() {
        player.PlayerMovement.SetKinematic(true);
    }

    public override void UpdateState() {
    }
    public override void ExitState() {
        player.PlayerMovement.SetKinematic(false);
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if (player.CurrentBasketball.CurrentState == Basketball.BasketballState.PickedUp) {
            return PlayerStateMachine.PlayerState.IdleDribble;
        }
        return PlayerStateMachine.PlayerState.PickUp;
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
