using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;
using TMPro;

public class Board : NinjaMonoBehaviour {
    public TextMeshProUGUI scoreText;
    [SerializeField] ParticleSystem scoreParticles; 
    [field: SerializeField] public Transform ThrowTarget {get; private set;}
    [SerializeField] GameObject selectionIndicator;
    public Action OnScore;
    int currentScore = 0;
    private void Awake() {
        scoreText.text = currentScore.ToString();
    }
    private void OnEnable() {
        HideIndicator();
        Player.OnPlayerScore += OnPlayerScore;
    }
    void OnPlayerScore(Player player, int score) {
        if(this!=player.LastThrowBoard) {
            return;
        }
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }
    public void OnValidScore() {
        var logId = "OnScore";
        logd(logId, "Player Scored!");
        OnScore?.Invoke();
    }

    public void ShowIndicator() => selectionIndicator.SetActive(true);
    public void HideIndicator() => selectionIndicator.SetActive(false);
}

