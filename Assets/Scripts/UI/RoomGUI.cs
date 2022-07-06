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

    [Header("Chatting")] 
    public ChattingScroll chattingScroll;

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
        // ��Ʈ��ũ 
        networkManager = FindObjectOfType<NetworkManager>();
        networkManager.onPlayerEnteredRoom += UpdateEnteredPlayerInfo;
        networkManager.onPlayerLeftRoom += UpdateLeftPlayerInfo;

        // �ʱ� ����
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

    private void Update()
    {
        // ó�� ���͸� ������ Input Field Ȱ��ȭ
        // Input Field Ȱ��ȭ�� ���¿��� ���͸� ������ 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // ä�� ó��
            Chat(chattingScroll.chatInputField.text);

            // �ʱ⿣ ä���� ġ�� ���°� �ƴϴ�
            //if (!isChatting)
            //{
            //    // ä�� Ȱ��ȭ
            //    chatInputField.Select();
            //}
            //// InputField�� ä���� ġ�� ���¸� ���� ������ �Է��� ��ģ��
            //else
            //{
            //    Chat("Player : " + chatInputField.text);
            //    chatInputField.Select();
            //}

            //// �ݴ�� ��ȯ
            //isChatting = !isChatting;
        }
    }

    public void Chat(string message)
    {
        photonView.RPC(nameof(Chat_RPC), RpcTarget.All, 
            PhotonNetwork.LocalPlayer.NickName + " : " + message);
    }

    [PunRPC]
    private void Chat_RPC(string message)
    {
        chattingScroll.Chat(message);
    }

    // �� �ȿ� �ִ� �÷��̾� ���� ����
    private void InitRoomPlayers()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        print(playerCount);
        
        // ���� �濡 �ִ� ����� ������ ����
        foreach (var player in PhotonNetwork.PlayerList)
        {
            print($"{player.NickName}'s actor number - {player.ActorNumber}");
            roomPlayers[player.ActorNumber - 1].SetPlayerInfo(player);
        }
        // �� �ܴ� ��� �� �� ó��
        for (int i = 0; i < roomPlayers.Length; i++)
        {
            if (!roomPlayers[i].isUsed)
            {
                roomPlayers[i].DisableUI();
            }
        }
    }

    // ���� ���� ����
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

    #region Game Select
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

        // ���� �ִϸ��̼� �߰�
        gameScreenShot.sprite = gameData.gameScreenShot;
        gameName.text = gameData.gameName;
        gameExplain.text = gameData.gameExplain;
    }
    #endregion

    #region For Game Start, Leave Button
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
    #endregion
}
