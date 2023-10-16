using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class UIManager : NinjaMonoBehaviour {
    [SerializeField] PlayerScore playerScore;
    public PlayerScore PlayerScore {get; private set;}
    public void AddPlayerScore(Player player) {
        var logId = "AddPlayerScore";
        if(player==null) {
            logw(logId, "Player is null => no-op");
        }
        PlayerScore = Instantiate(playerScore);
        PlayerScore.SetPlayer(player);

    }
}
