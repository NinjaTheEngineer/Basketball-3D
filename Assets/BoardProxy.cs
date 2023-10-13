using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class BoardProxy : NinjaMonoBehaviour {
    [SerializeField] Board targetBoard;

    public Board GetTargetBoard() => targetBoard;
}
