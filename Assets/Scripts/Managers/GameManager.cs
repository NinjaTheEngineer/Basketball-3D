using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using Cinemachine;
using System;

public class GameManager : NinjaMonoBehaviour {
    public enum GameMode { SINGLE, DUEL }
    public static GameManager Instance;
    public AddressableInstantiator courtInstantiator, playerInstantiator;
    public Action OnGameStart;
    public Basketball basketball;
    public Basketball Basketball {get; private set; }
    public GameMode CurrentGameMode { get; private set; }

    public Vector3 playerInitPos;
    public Vector3 secPlayerInitPos;
    public float basketballStartHeight = 5f;
    public bool startGameOnAwake=false;
    private void Awake() {
        if(Instance) {
            Destroy(gameObject);
            return;
        }
        Utils.InitRandom();
        Instance = this;
    }
    private void Start() {
        if(startGameOnAwake) {
            StartGame();
        }
    }
    public void LoadGame() {
        SceneManager.Instance.OpenScene(SceneName.Game);
        StartCoroutine(LoadGameRoutine());
    }
    public void StartGame() {
        Basketball = Instantiate(basketball, new Vector3(0, basketballStartHeight, 0), Quaternion.identity);
        OnGameStart?.Invoke();
        AudioManager.Instance.PlayStartGameSound();
    }
    public float startGameRoutineIntervals = 0.1f;
    IEnumerator LoadGameRoutine() {
        var waitForSeconds = new WaitForSeconds(startGameRoutineIntervals);        
        while(!SceneManager.Instance.SceneLoaded) {
            yield return waitForSeconds;
        }
        courtInstantiator.InstantiateAssetReference();
        playerInstantiator.InstantiateAssetReference(playerInitPos);
        if(CurrentGameMode==GameMode.DUEL) {
            yield break;
        }
        StartGame();
    }

    public void RestartGame() => LoadGame();

    public void SetDuel() => CurrentGameMode=GameMode.DUEL;
}
