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
    float startTime = 0;
    public float throwTime = 0.75f;
    public override void EnterState() {
        player.PlayerMovement.SetKinematic(true);
        player.ShowMeterRoutine();
        startTime = 0;
    }

    public override void UpdateState() {
        if (player.PlayerInput.ThrowInput && !player.PowerMeter.IsRunning) {
            player.PowerMeter.StartMeter();
        } else if (player.PlayerInput.ThrowReleaseInput && player.PowerMeter.IsRunning) {
            var force = player.PowerMeter.StopMeter();
            player.PlayerThrower.SetThrowForce(force);
            player.anim.SetBool("CanThrow", true);
            startTime = Time.realtimeSinceStartup;
        }
    }

    public override void ExitState() {
        startTime = 0;
        player.anim.SetBool("CanThrow", false);
        player.PlayerMovement.SetKinematic(false);
    }

    public override PlayerStateMachine.PlayerState GetNextState() {
        if (startTime!=0 && Time.realtimeSinceStartup - startTime > throwTime) {
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
}