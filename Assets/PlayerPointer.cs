using System.Collections;
using System.Collections.Generic;
using NinjaTools;
using UnityEngine;

public class PlayerPointer : NinjaMonoBehaviour {
    [SerializeField] Transform orientation;
    [SerializeField] float boardCheckDelay = 0.2f;
    private Board _targetBoard;
    public Board TargetBoard { 
        get => _targetBoard;
        private set {
            var logId = "TargetBoard_set";
            if(value==_targetBoard) {
                return;
            }
            if(_targetBoard) {
                _targetBoard.HideIndicator();
            }
            logd(logId, "Setting TargetBoard from "+_targetBoard.logf() + " to "+value.logf());
            _targetBoard = value;
            if (_targetBoard) {
                _targetBoard.ShowIndicator();
            }
        }
    }
    private void Start() => StartCoroutine(GetTargetBoardRoutine());

    public IEnumerator GetTargetBoardRoutine() {
        var logId = "GetTargetBoardRoutine";
        var waitForSeconds = new WaitForSeconds(boardCheckDelay);
        while(true) {
            RaycastHit raycastHit;
            if(Physics.Raycast(orientation.position, Camera.main.transform.forward, out raycastHit, 20)) {
                var collider = raycastHit.collider;
                TargetBoard = collider.GetComponent<Board>()??collider.GetComponentInParent<BoardProxy>()?.GetTargetBoard();
                logd(logId,"Collider hit="+collider.name);
            } else {
                TargetBoard = null;
            }
            yield return waitForSeconds;
        }
    }
}
