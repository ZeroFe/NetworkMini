using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AquaSpawnManager : MonoBehaviour
{
    public static AquaSpawnManager Instance { get; private set; }

    public Transform[] spawnPoses;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    public void Spawn()
    {
        var playerNum = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var player = PhotonNetwork.Instantiate("Aqua/Player", spawnPoses[playerNum].position, Quaternion.identity);
        print($"Spawn player - {playerNum}");
        Camera.main.GetComponent<CameraFollow>().SetTarget(player.transform);
    }
}