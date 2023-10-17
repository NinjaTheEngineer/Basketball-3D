using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using Photon.Pun;

public class SpawnPlayers : NinjaMonoBehaviour {
    public GameObject playerPrefab;
    public List<Vector3> playerSpawnPosition;

    private void Start() {
        var logId = "Start";
        var isMasterClient = PhotonNetwork.IsMasterClient;
        int posIndex = 1;
        if(isMasterClient) {
            posIndex = 0;
        }
        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition[posIndex], Quaternion.identity);
        logd(logId, "IsMasterClient="+isMasterClient);
    }
}
