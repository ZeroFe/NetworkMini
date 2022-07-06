using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoUpSpawnManager : MonoBehaviour
{
    public static GoUpSpawnManager Instance { get; private set; }

    public Transform spawnPos;
    public float playerPosBonus = 2.0f;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn()
    {
        var playerNum = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var player = PhotonNetwork.Instantiate("GoUp/Player", spawnPos.position, Quaternion.identity);
        player.transform.Translate(Vector3.right * playerNum * playerPosBonus); 
        print($"Spawn player - {playerNum}");
    }
}
