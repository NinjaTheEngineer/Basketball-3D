using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using Cinemachine;
using System;

public class GameManager : NinjaMonoBehaviour {
    public static GameManager Instance;
    public AddressableInstantiator courtInstantiator, playerInstantiator;
    public Action OnGameStart;
    public bool startGameOnAwake=false;
    private void Awake() {
        if(Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start() {
        if(startGameOnAwake) {
            StartGame();
        }
    }
    public void LoadGame() {
        StartCoroutine(LoadGameRoutine());
    }
    private void StartGame() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnGameStart?.Invoke();
    }
    public float startGameRoutineIntervals = 0.1f;
    IEnumerator LoadGameRoutine() {
        var waitForSeconds = new WaitForSeconds(startGameRoutineIntervals);
        SceneManager.Instance.OpenScene(SceneName.Game);
        
        while(!SceneManager.Instance.SceneLoaded) {
            yield return waitForSeconds;
        }
        courtInstantiator.InstantiateAssetReference();
        playerInstantiator.InstantiateAssetReference();
        StartGame();
    }
}
