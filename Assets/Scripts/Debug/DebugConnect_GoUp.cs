using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class DebugConnect_GoUp : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Debug - connected");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        var option = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = true,
            IsOpen = true
        };
        PhotonNetwork.JoinOrCreateRoom("Test01", option, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // 플레이어 생성
        PhotonNetwork.LocalPlayer.NickName = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
        GoUpSpawnManager.Instance.Spawn();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // 디버그용
        // 방장이면 시작 명령
        //if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            GoUpGameSystem.Instance.StartGame();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        var option = new RoomOptions
        {
            MaxPlayers = 3,
            IsVisible = true,
            IsOpen = true
        };
        PhotonNetwork.JoinOrCreateRoom("Test02", option, TypedLobby.Default);
    }
}
