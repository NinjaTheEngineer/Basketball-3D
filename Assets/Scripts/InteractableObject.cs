using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public abstract class InteractableObject : NinjaMonoBehaviour {
    public NearIndicator nearIndicator;
    public abstract void OnInteract(GameObject interactor);
    public virtual void Awake() {
        nearIndicator = Instantiate(nearIndicator);
        nearIndicator?.Hide();
    }
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerExit(Collider other);
}
