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
    private void OnEnable() {
        HideIndicator();
        scoreValidator.OnScoreValid += OnScore; 
    }
    private void OnDisable() {
        scoreValidator.OnScoreValid -= OnScore;
    }
    void OnScore() {
        var logId = "OnScore";
        logd(logId, "Player Scored!");
        //scoreParticles?.Play();
    }

    public void ShowIndicator() => selectionIndicator.SetActive(true);
    public void HideIndicator() => selectionIndicator.SetActive(false);
}

