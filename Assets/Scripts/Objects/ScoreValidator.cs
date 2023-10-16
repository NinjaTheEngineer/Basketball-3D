using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreValidator : Validator {
    public TextMeshProUGUI scoreText;
    public static Action OnScoreValid;
    public Action OnScoreInvalid;
    public FirstScoreValidator topValidator;
    bool isThrowInvalid;
    int currentScore = 0;
    [SerializeField] float invalidThrowDelay = 2f;
    
    private void Awake() {
        scoreText.text = currentScore.ToString();
    }
    private void OnTriggerEnter(Collider other) {
        var logId = "OnTriggerEnter";
        IsValid = topValidator.IsValid && !isThrowInvalid;
        if (IsValid) {
            logd(logId, "Score is valid!");
            StartCoroutine(ValidScoreRoutine());
        } else {
            logd(logId, "Score is invalid => InvalidScoreRoutine");
            StopAllCoroutines();
            StartCoroutine(InvalidScoreRoutine());
        }
    }
    IEnumerator ValidScoreRoutine() {
        var logId = "ValidScoreRoutine";
        OnScoreValid?.Invoke();
        currentScore += 2;
        scoreText.text = currentScore.ToString();
        isThrowInvalid = true;
        yield return new WaitForSeconds(invalidThrowDelay);
        logd(logId, "Throw no longer invalid.");
        isThrowInvalid = false;
    }
    IEnumerator InvalidScoreRoutine() {
        var logId = "InvalidThrowRoutine";
        OnScoreInvalid?.Invoke();
        isThrowInvalid = true;
        yield return new WaitForSeconds(invalidThrowDelay);
        logd(logId, "Throw no longer invalid.");
        isThrowInvalid = false;
    }
}