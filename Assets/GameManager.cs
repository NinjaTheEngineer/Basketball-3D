using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using Cinemachine;
using System;

public class GameManager : NinjaMonoBehaviour {
    public static GameManager Instance;
    public CinemachineFreeLook menuCamera;
    public AddressableInstantiator courtInstantiator, playerInstantiator;
    public GameObject startMenu;
    public Action OnGameStart;
    private void Awake() {
        if(Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start() {
        courtInstantiator.InstantiateAssetReference();
        playerInstantiator.InstantiateAssetReference();
    }
    Player player;
    public void StartGame() {
        player = playerInstantiator.InstanceReference.GetComponent<Player>();
        StartCoroutine(StartGameRoutine());
    }
    public float startGameRoutineIntervals = 0.1f;
    IEnumerator StartGameRoutine() {
        var waitForSeconds = new WaitForSeconds(startGameRoutineIntervals);
        menuCamera.gameObject.SetActive(false);
        yield return waitForSeconds;
        startMenu.SetActive(false);
        OnGameStart?.Invoke();

    }
}
