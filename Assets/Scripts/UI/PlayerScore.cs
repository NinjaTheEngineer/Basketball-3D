using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;

public class PlayerScore : NinjaMonoBehaviour {
    public TextMeshProUGUI scoreText;
    public int CurrentScore { get; private set; }
    Player player;
    public void SetPlayer(Player p) {
        player = p;
    }
    private void OnEnable() {
        scoreText.text = CurrentScore.ToString();
        Player.OnPlayerScore += OnPlayerScore;
    }
    private void OnDisable() {
        Player.OnPlayerScore -= OnPlayerScore;
    }
    public void OnPlayerScore(Player p, int score) {
        var logId = "OnPlayerScore";
        if(p==null || p!=player) {
            logw(logId,"Player="+player.logf()+" PlayerThatScored="+p.logf());
            return;
        }
        AudioManager.Instance.PlayScoreSound();
        CurrentScore += score;
        scoreText.text = CurrentScore.ToString();
    }
}
