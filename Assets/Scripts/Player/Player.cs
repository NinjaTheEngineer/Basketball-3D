using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class Player : NinjaMonoBehaviour {
    [field: SerializeField] public PlayerStateMachine PlayerStateMachine { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public Basketball CurrentBasketball { get; private set; }
    public Transform basketballHolder;
    public Transform ballPickupPos;
    public Animator anim;

    private void Awake() {
        PlayerStateMachine = GetComponent<PlayerStateMachine>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
        basketballHolder.gameObject.SetActive(false);
    }
    public void ThrowBasketball() {
        CurrentBasketball.transform.position = basketballHolder.position;
        basketballHolder.gameObject.SetActive(false);
        CurrentBasketball.Throw(transform.forward);
        CurrentBasketball = null;
    }
    private void Update() {
        if(PlayerInput.InteractInput) {
            if(CurrentBasketball==null) {
                Collider[] colliders = Physics.OverlapSphere(ballPickupPos.position, 1f);
                var collidersCount = colliders.Length;
                for (int i = 0; i < collidersCount; i++) {
                    InteractableObject interactableObject = colliders[i].GetComponent<InteractableObject>();
                    if(interactableObject!=null) {
                        interactableObject.OnInteract(gameObject);
                        break;
                    }
                }
            } else if(CurrentBasketball.CurrentState==Basketball.BasketballState.PickedUp){
                PlayerStateMachine.TransitionToState(PlayerStateMachine.PlayerState.Throw);
            }
        }
    }
    public void PickUpBall(Basketball basketball) {
        var logId = "PickUpBall";
        if (basketball == null) {
            logw(logId, "Basketball is null => no-op");
            return;
        }
        basketball.transform.position = ballPickupPos.position;
        var ballPos = basketball.transform.position;
        var ballDir = ballPos - transform.position;
        CurrentBasketball = basketball;
        logd(logId, "Picking up basketball=" + basketball.logf());
        ballDir.y = 0;
        transform.forward = ballDir;
        PlayerStateMachine.TransitionToState(PlayerStateMachine.PlayerState.PickUp);
    }

    public void OnPickUpBasketball() {
        var logId = "OnPickUpBasketball";
        if (CurrentBasketball == null) {
            return;
        }
        logd(logId, "Picked Basketball=" + CurrentBasketball.logf());
        basketballHolder.gameObject.SetActive(true);
        CurrentBasketball.OnPickedUp();
    }
}
