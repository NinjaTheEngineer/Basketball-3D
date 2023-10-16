using System;
using System.Collections;
using UnityEngine;

public class ScoreValidator : Validator {
    Board board;
    public FirstScoreValidator topValidator;
    bool isThrowInvalid;
    [SerializeField] float invalidThrowDelay = 2f;

    private void Start() {
        board = GetComponentInParent<Board>();
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
        board.OnValidScore();
        isThrowInvalid = true;
        yield return new WaitForSeconds(invalidThrowDelay);
        logd(logId, "Throw no longer invalid.");
        isThrowInvalid = false;
    }
    IEnumerator InvalidScoreRoutine() {
        var logId = "InvalidThrowRoutine";
        isThrowInvalid = true;
        yield return new WaitForSeconds(invalidThrowDelay);
        logd(logId, "Throw no longer invalid.");
        isThrowInvalid = false;
    }
}