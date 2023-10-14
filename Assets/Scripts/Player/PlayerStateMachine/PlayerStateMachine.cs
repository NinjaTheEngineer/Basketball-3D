using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System.Linq;
using System;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState> {
    Player player;
    public enum PlayerState {
        Idle,
        IdleDribble,
        Run,
        RunDribble,
        Throw,
        PickUp
    }
    
    private void Awake() {
        player = GetComponent<Player>();
        SetStates();
    }
    void SetStates() {
        States.Add(PlayerState.Idle, new IdleState(player, PlayerState.Idle));
        States.Add(PlayerState.IdleDribble, new IdleDribbleState(player, PlayerState.IdleDribble));
        States.Add(PlayerState.Run, new RunState(player, PlayerState.Run));
        States.Add(PlayerState.RunDribble, new RunDribbleState(player, PlayerState.RunDribble));
        States.Add(PlayerState.Throw, new ThrowState(player, PlayerState.Throw));
        States.Add(PlayerState.PickUp, new PickUpState(player, PlayerState.PickUp));
        CurrentState = States[PlayerState.Idle];
    }
    public void StartThrowBasketball() {
        TransitionToState(PlayerState.Throw);
    }
    public override void OnStateChange(PlayerState state) {
        player.anim.Play(state.ToString());
    }
}
