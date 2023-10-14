using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using UnityEngine.UI;

public class PowerMeter : NinjaMonoBehaviour {
    public Slider slider;
    public Image fillImage;
    float holdDownStartTime;
    public bool IsRunning { get; private set; }
    [SerializeField] float maxHoldDownTime = 2f;
    [Range(0, 1)]
    [SerializeField] float minFillAlpha = 0.5f;
    [Range(0, 1)]
    [SerializeField] float maxFillAlpha = 0.9f;
    float holdDownTime;
    private void Awake() {
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        StartCoroutine(HandleSliderRoutine());
    }
    IEnumerator HandleSliderRoutine() {
        var logId = "HandleSliderRoutine";
        logd(logId, "Startin routine!");
        var waitForSeconds = new WaitForSeconds(0.1f);
        while(true) {
            if(!IsRunning) {
                yield return waitForSeconds;
                continue;
            }
            holdDownTime = holdDownTime < maxHoldDownTime ? Time.time - holdDownStartTime : maxHoldDownTime;
            logd(logId, "HoldDownTime="+holdDownTime, true);
            var force = CalculateHoldDownForce(holdDownTime);
            slider.value = force;
            ControlAlpha(force);
            yield return waitForSeconds;
        } 
    }
    private void OnDisable() {
        ControlAlpha(0);
        StopAllCoroutines();
    }
    void ControlAlpha(float force) {
        var logId = "ControlAlpha";
        var color = fillImage.color;
        color.a = Mathf.Lerp(minFillAlpha, maxFillAlpha, force);
        logd(logId, "ColorAlpha="+color.a);
        fillImage.color = color;
    }
    public void StartMeter() {
        var logId = "StartMeter";
        logd(logId, "Starting meter!");
        holdDownStartTime = Time.time;
        holdDownTime = 0;
        IsRunning = true;
    }
    public float StopMeter() {
        var logId = "StopMeter";
        IsRunning = false;
        var force = CalculateHoldDownForce(holdDownTime);
        logd(logId, "Stopping Meter with holdDownTime="+holdDownTime+" Force="+force, true);
        return force;
    }
    float CalculateHoldDownForce(float holdTime) => Mathf.Clamp01(holdTime / maxHoldDownTime);
}
