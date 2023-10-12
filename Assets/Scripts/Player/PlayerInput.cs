using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class PlayerInput : NinjaMonoBehaviour {
    public float HorizontalInput => Input.GetAxisRaw("Horizontal");
    public float VerticalInput => Input.GetAxisRaw("Vertical");
    public bool InteractInput => Input.GetKeyDown(KeyCode.E);
}
