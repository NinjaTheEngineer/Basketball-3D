using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class BasketballHolder : NinjaMonoBehaviour {
    public GameObject visu;
    void Start() => HideBasketball();

    // Update is called once per frame
    public void ShowBasketball() => visu.SetActive(true);
    public void HideBasketball() => visu.SetActive(false);
}
