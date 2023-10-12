using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using Unity.PlasticSCM.Editor;

public class ThrowState : BaseState<PlayerStateMachine.PlayerState> {
    Player player;
    public ThrowState(Player player, PlayerStateMachine.PlayerState key) : base(key) {
        this.player = player;
    }
    float startTime;
    public float throwTime = 0.75f;
    public override void EnterState() {
        player.PlayerMovement.SetKinematic(true);
        startTime = Time.realtimeSinceStartup;
    }

    public override void ExitState() {
        player.PlayerMovement.SetKinematic(false);
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if (Time.realtimeSinceStartup - startTime > throwTime) {
            return PlayerStateMachine.PlayerState.Idle;
        }
        return PlayerStateMachine.PlayerState.Throw;
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