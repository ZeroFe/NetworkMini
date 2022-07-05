using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomGUI : MonoBehaviourPun
{
    public TextMeshProUGUI roomName;


    [Header("Player")]
    public RoomPlayerInfoUI[] roomPlayers;

    [Header("Game Explain")] 
    public Image gameScreenShot;
    public TextMeshProUGUI gameName;
    public TextMeshProUGUI gameExplain;
    public GameData[] gameDatas;
    private int gameDataIndex = 0;

    [Header("Buttons")]
    public Button prevGameButton;
    public Button nextGameButton;
    public Button gameStartButton;

    private NetworkManager networkManager;

    void OnEnable()
    {
        // 네트워크 
        networkManager = FindObjectOfType<NetworkManager>();
        networkManager.onPlayerEnteredRoom += UpdateEnteredPlayerInfo;
        networkManager.onPlayerLeftRoom += UpdateLeftPlayerInfo;

        // 초기 세팅
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        InitRoomPlayers();
        SetRoomMasterSetting();

        UpdateGameExplain();
    }

    private void OnDisable()
    {
        networkManager.onPlayerEnteredRoom -= UpdateEnteredPlayerInfo;
        networkManager.onPlayerLeftRoom -= UpdateLeftPlayerInfo;
    }

    // 룸 안에 있는 플레이어 정보 설정
    private void InitRoomPlayers()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        print(playerCount);
        
        // 기존 방에 있는 멤버들 정보를 받음
        foreach (var player in PhotonNetwork.PlayerList)
        {
            print($"{player.NickName}'s actor number - {player.ActorNumber}");
            roomPlayers[player.ActorNumber - 1].SetPlayerInfo(player);
        }
        // 그 외는 사용 안 함 처리
        for (int i = 0; i < roomPlayers.Length; i++)
        {
            if (!roomPlayers[i].isUsed)
            {
                roomPlayers[i].DisableUI();
            }
        }
    }

    // 방장 정보 설정
    private void SetRoomMasterSetting()
    {
        bool isMasterClient = PhotonNetwork.LocalPlayer.IsMasterClient;

        gameStartButton.interactable = isMasterClient ? true : false;
        prevGameButton.interactable = isMasterClient ? true : false;
        nextGameButton.interactable = isMasterClient ? true : false;
    }

    private void UpdateEnteredPlayerInfo(Photon.Realtime.Player newPlayer)
    {
        roomPlayers[newPlayer.ActorNumber-1].SetPlayerInfo(newPlayer); 
    }

    private void UpdateLeftPlayerInfo(Photon.Realtime.Player otherPlayer)
    {
        roomPlayers[otherPlayer.ActorNumber - 1].DisableUI();
    }

    public void SetPrevGame()
    {
        photonView.RPC(nameof(SetPrevGameRPC), RpcTarget.All);
    }

    [PunRPC]
    private void SetPrevGameRPC()
    {
        if (--gameDataIndex < 0)
        {
            gameDataIndex += gameDatas.Length;
        }
        UpdateGameExplain();
    }

    public void SetNextGame()
    {
        photonView.RPC(nameof(SetNextGameRPC), RpcTarget.All);
    }

    [PunRPC]
    private void SetNextGameRPC()
    {
        gameDataIndex = (gameDataIndex + 1) % gameDatas.Length;
        UpdateGameExplain();
    }

    private void UpdateGameExplain()
    {
        var gameData = gameDatas[gameDataIndex];

        // 이후 애니메이션 추가
        gameScreenShot.sprite = gameData.gameScreenShot;
        gameName.text = gameData.gameName;
        gameExplain.text = gameData.gameExplain;
    }

    public void StartGame()
    {
        var gameNumber = gameDataIndex != 0 ? gameDataIndex : UnityEngine.Random.Range(1, gameDatas.Length);
        var gameData = gameDatas[gameNumber];
        PhotonNetwork.LoadLevel(gameData.gameScene.name);
    }

    public void LeaveRoom()
    {
        networkManager.LeaveRoom();
    }
}
