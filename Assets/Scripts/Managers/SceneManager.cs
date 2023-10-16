using System;
using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public enum SceneName {
    MainMenu,
    Game,
    Two
}
public class SceneManager : NinjaMonoBehaviour {
    private Animator animator;
    public static SceneManager Instance { get; private set; }
    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OpenScene(SceneName scene) {
        animator.Play("OpenScene");
        string sceneName = scene.ToString();
        StartCoroutine(OpenSceneRoutine(sceneName));
    }

    public void OpenScene(string sceneName) {
        var logId = "OpenScene";
        if (sceneName == null) {
            logw(logId, "Tried to open null scene => no-op");
            return;
        }
        StartCoroutine(OpenSceneRoutine(sceneName));
    }

    public void RestartScene() {
        var logId = "RestartScene";
        logd(logId, "Restarting Scene");
        var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        StartCoroutine(OpenSceneRoutine(activeScene.name));
    }
    public float openSceneDelay = 0.1f;
    public float closeSceneDelay = 0.5f;
    public float sceneLoadDelay = 0.1f;
    IEnumerator OpenSceneRoutine(string sceneName) {
        var logId = "OpenSceneRoutine";
        SceneLoaded = false;
        var openSceneWaitForSeconds = new WaitForSeconds(openSceneDelay);
        var closeSceneWaitForSeconds = new WaitForSeconds(closeSceneDelay);
        var sceneLoadWaitForSeconds = new WaitForSeconds(sceneLoadDelay);
        logd(logId, "Opening Scene=" + sceneName);

        animator.Play("CloseScene");
        yield return closeSceneWaitForSeconds;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        while(!SceneLoaded) {
            yield return sceneLoadWaitForSeconds;
        }
        yield return openSceneWaitForSeconds;
        animator.Play("OpenScene");
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1) {
        SceneLoaded = true;
    }

    public bool SceneLoaded { get; private set;}
}