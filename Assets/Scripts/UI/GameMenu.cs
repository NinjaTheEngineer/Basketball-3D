using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;

public class GameMenu : NinjaMonoBehaviour {
    public TextMeshProUGUI menuTitle;
    public GameObject resumeButton;
    public bool IsPaused {get; private set;}
    private void Awake() {
        Hide();
    }
    public void ShowPauseMenu() {
        menuTitle.text = "Pause";
        resumeButton.SetActive(true);
        IsPaused = true;
        Show();
    }
    public void ShowGameOver() {
        menuTitle.text = "Game Over";
        resumeButton.SetActive(false);
        Show();
    }

    public void Show() {
        var logId = "Show";
        logd(logId, "Show");
        gameObject.SetActive(true);
        IsPaused = false;
        UnlockCursor();
        Time.timeScale = 0f;
    }
    public void QuitGame() {
        AudioManager.Instance.PlayButtonClick();
        Hide();
        SceneManager.Instance.OpenScene(SceneName.MainMenu);
    }
    public void RestartGame() {
        AudioManager.Instance.PlayButtonClick();
        Hide();
        GameManager.Instance.RestartGame();
    }
    public void ResumeGame() {
        AudioManager.Instance.PlayButtonClick();
        Hide();
        LockCursor();
    }
    public void Hide() {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void LockCursor() {
        var logId = "LockCursor";
        logd(logId, "Lock Cursor!");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public void UnlockCursor() {
        var logId = "UnlockCursor";
        logd(logId, "Unlock Cursor!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
