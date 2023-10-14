using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class PlayerInput : NinjaMonoBehaviour {
    public float HorizontalInput => Input.GetAxisRaw("Horizontal");
    public float VerticalInput => Input.GetAxisRaw("Vertical");
    public bool InteractInput => Input.GetKeyDown(KeyCode.E);

    public bool ThrowInput => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    public bool ThrowReleaseInput => Input.GetMouseButtonUp(0);
}
