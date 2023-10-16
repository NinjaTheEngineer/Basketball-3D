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
        AudioManager.Instance.PlayThrowSound();
        player?.ThrowBasketball();
    }

    public void OnBasketballBounce() {
        AudioManager.Instance.PlayBounceSound();
    }
}
