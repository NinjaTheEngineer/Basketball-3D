using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class UIManager : NinjaMonoBehaviour {
    public static UIManager Instance;
    [SerializeField] CountdownTimer countdownTimer;
    [SerializeField] PlayerScore playerScore;
    List<PlayerScore> playerScores = new List<PlayerScore>(2);
    public GameObject playerScoresHolder;
    public GameMenu gameMenuPb;
    GameMenu gameMenu;

    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void OnGameStart() {
        gameMenu = Instantiate(gameMenuPb, transform);
        gameMenu.LockCursor();
        countdownTimer = Instantiate(countdownTimer, transform);
        CountdownTimer.OnCountdownFinished += OnGameOver;
        countdownTimer.StartTimer();
    }
    private void OnEnable() {
        GameManager.Instance.OnGameStart += OnGameStart;
        Player.OnPlayerScore += OnPlayerScore;
    }
    private void OnDisable() {
        GameManager.Instance.OnGameStart -= OnGameStart;
        Player.OnPlayerScore -= OnPlayerScore;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            var paused = gameMenu.IsPaused;
            if(paused) {
                gameMenu.Hide();
            } else {
                gameMenu.ShowPauseMenu();
            }
        }
    }

    void OnGameOver() {
        AudioManager.Instance.PlayGameOverSound();
        gameMenu.ShowGameOver();
    }
    public void OnPlayerScore(Player player, int score) {
        countdownTimer.StartTimer();
    }
    public void CreatePlayerScoreGUI(Player player) {
        var logId = "CreatePlayerScoreGUI";
        if(player==null) {
            logw(logId, "Player is null => no-op");
        }
        var ps = Instantiate(playerScore, playerScoresHolder.transform);
        ps.SetPlayer(player);
        playerScores.Add(ps);
    }
}
