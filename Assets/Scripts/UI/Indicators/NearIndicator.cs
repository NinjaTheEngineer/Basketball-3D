using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(RotateTowardsCamera))]
public class NearIndicator : NinjaMonoBehaviour {
    [SerializeField] NearIndicatorSO nearIndicatorConfig;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
    private void Awake() {
        image.gameObject.SetActive(false);

        if (nearIndicatorConfig==null) {
            Destroy(gameObject);
            return;
        }
    }
    private void Start() {
        image.sprite = nearIndicatorConfig.sprite;
        text.text = nearIndicatorConfig.text;
    }

    private void Update() {
        if(image.isActiveAndEnabled) {
            transform.position = followTarget.position;
        }
    }
    Transform followTarget;
    public void Show(Transform target) {
        followTarget = target;
        image.gameObject.SetActive(true);
    }
    public void Hide() {
        image.gameObject.SetActive(false);
    }
}
