using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerAnimationEvents : NinjaMonoBehaviour {
    Player player;
    private void Awake() {
        player = GetComponentInParent<Player>();
    }

    public void OnPickUpBall() {
        player?.OnPickUpBasketball();
    }

    public void OnThrowBasketball() {
        player?.ThrowBasketball();
    }
}
