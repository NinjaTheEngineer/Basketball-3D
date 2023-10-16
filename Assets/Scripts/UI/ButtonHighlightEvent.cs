using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonHighlightEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public RotateObject rotateObject;
    public void OnPointerEnter(PointerEventData eventData) {
        rotateObject.ChangeDirection();
    }

    public void OnPointerExit(PointerEventData eventData) {
    }
}