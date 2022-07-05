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
        // ���� ������ ä��â Ȱ��ȭ
    }

    public void QuickJoin()
    {
        networkManager.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        // �� �����
        networkManager.CreateRoom();
    }
}
