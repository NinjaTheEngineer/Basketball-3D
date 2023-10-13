using System.Collections;
using UnityEngine;
public class FirstScoreValidator : Validator {
    [SerializeField] float invalidateDelay = 1f;
    private void OnTriggerEnter(Collider other) {
        var logId = "OnTriggerEnter";
        StopAllCoroutines();
        logd(logId, "Trigger entered with "+other.gameObject.name);
        IsValid = true;
        StartCoroutine(InvalidateRoutine());
    }
    IEnumerator InvalidateRoutine() {
        var logId = "InvalidateRoutine";
        yield return new WaitForSeconds(invalidateDelay);
        IsValid = false;
        logd(logId, "Score no longer Valid.");
    }
}