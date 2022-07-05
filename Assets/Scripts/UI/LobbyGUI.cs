using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGUI : MonoBehaviour
{
    public NetworkManager networkManager;

    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();


    }

    void Update()
    {
        // 엔터 누르면 채팅창 활성화
    }

    public void QuickJoin()
    {
        networkManager.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        // 방 만들기
        networkManager.CreateRoom();
    }
}
