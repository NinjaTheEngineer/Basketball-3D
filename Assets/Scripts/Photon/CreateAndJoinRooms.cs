using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks {
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom() {
        var logId = "CreateRoom";
        var text = createInput.text;
        if(string.IsNullOrEmpty(text)) {
            Utils.logw(logId, "Invalid text => no-op");
            return;
        }
        PhotonNetwork.CreateRoom(text);
    }
    public void JoinRoom() {
        var logId = "JoinRoom";
        var text = joinInput.text;
        if(string.IsNullOrEmpty(text)) {
            Utils.logw(logId, "Invalid text => no-op");
            return;
        }
        PhotonNetwork.JoinRoom(text);
    }

    public override void OnJoinedRoom() {
        GameManager.Instance.SetDuel();
        PhotonNetwork.LoadLevel(SceneName.Duel.ToString());
    }
}
