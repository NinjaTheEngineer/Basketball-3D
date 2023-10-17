using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class StartMenu : NinjaMonoBehaviour {
    public void OnStartButtonClick() {
        AudioManager.Instance.PlayBounceSound();
        GameManager.Instance.LoadGame();
    }
    public void OnDuelButtonClick() {
        AudioManager.Instance.PlayBounceSound();
        SceneManager.Instance.OpenLoadingScene();
    }
}
