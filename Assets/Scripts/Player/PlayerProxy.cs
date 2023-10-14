using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProxy : MonoBehaviour {
    public Transform board;
    public float distance;
    void Update() {
        distance = Vector3.Distance(transform.position, board.position);
    }
}
