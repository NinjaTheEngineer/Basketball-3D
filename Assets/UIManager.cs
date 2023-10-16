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
    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable() {
        Player.OnPlayerScore += OnPlayerScore;
    }

    private void Start() {
        countdownTimer = Instantiate(countdownTimer, transform);
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
