using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;

public class Board : NinjaMonoBehaviour {
    [SerializeField] ParticleSystem scoreParticles; 
    [field: SerializeField] public Transform ThrowTarget {get; private set;}
    [SerializeField] ScoreValidator scoreValidator;
    [SerializeField] GameObject selectionIndicator;
    public Action OnScore;
    private void OnEnable() {
        HideIndicator();
        scoreValidator.OnScoreValid += OnBasketballScore; 
    }
    private void OnDisable() {
        scoreValidator.OnScoreValid -= OnBasketballScore;
    }
    void OnBasketballScore() {
        var logId = "OnScore";
        logd(logId, "Player Scored!");
        OnScore?.Invoke();
    }

    public void ShowIndicator() => selectionIndicator.SetActive(true);
    public void HideIndicator() => selectionIndicator.SetActive(false);
}

