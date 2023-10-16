using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;
using System;

public class CountdownTimer : NinjaMonoBehaviour {
    public TextMeshProUGUI timerText;
    public float countdownDuration = 24.0f;
    private float remainingTime;
    private bool isRunning;
    public static Action OnCountdownFinished;
    public void StartTimer() {
        isRunning = true;
        remainingTime = countdownDuration;
    }

    public void PauseTimer() {
        isRunning = false;
    }

    public void ResumeTimer() {
        isRunning = false;
    }

    private void Update() {
        if(!isRunning) {
            return;
        }
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0) {
            isRunning = false;
            remainingTime = 0;
            OnCountdownFinished?.Invoke();
        }
        int milliseconds = Mathf.FloorToInt(remainingTime * 1000);
        timerText.text = (milliseconds / 1000)+"."+(milliseconds % 100).ToString("D2");
    
    }
}