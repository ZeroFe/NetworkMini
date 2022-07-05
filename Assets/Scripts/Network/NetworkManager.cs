using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }

    public string UserName { get; set; }

    [Header("Login")] 
    [SerializeField] private GameObject loginPanel;
    [Header("Lobby")]
    [SerializeField] private GameObject lobbyPanel;
    [Header("Room")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private TextMeshProUGUI roomInfoText;

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI connectStateText;

    List<RoomInfo> roomInfos = new List<RoomInfo>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        connectStateText.text = "Disconnected";
    }

    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = UserName;
        PhotonNetwork.ConnectUsingSettings();
        print($"Connect - {UserName}");
    }

    public override void OnConnectedToMaster()
    {
        print("Connected");
        PhotonNetwork.JoinLobby();
        connectStateText.text = "Connected";
    }

    public override void OnJoinedLobby()
    {
        lobbyPanel.SetActive(true);
        connectStateText.text = "Joined Lobby";
    }

    public void CreateRoom()
    {
        //roomInput.text = string.IsNullOrEmpty(roomInput.text)
        //    ? "Room" + Random.Range(0, 100)
        //    : roomInput.text;
        PhotonNetwork.CreateRoom("Room Name", new RoomOptions { MaxPlayers = 3 });
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        connectStateText.text = "In Room";
    }

    public void SetRoomInfo()
    {
        roomInfoText.text = PhotonNetwork.CurrentRoom.Name + "/" +
                            PhotonNetwork.CurrentRoom.PlayerCount +
                            " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        photonView.RPC(nameof(ChatRPC), RpcTarget.All,
            newPlayer.NickName + "가 입장했습니다");
    }

    //랜덤으로 들어갈 방이 없을 때
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectStateText.text = "Fail Join Random Room";
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

    }

    public void SendChatMessage()
    {
        photonView.RPC(nameof(ChatRPC), RpcTarget.All, PhotonNetwork.NickName + ":" + chatInput.text);
        chatInput.text = "";
    }

    [PunRPC]
    void ChatRPC(string message)
    {
        //chatManager.Chat(message);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
