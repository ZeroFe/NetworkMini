using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class RoomGUI : MonoBehaviour
{
    public TextMeshProUGUI roomName;

    private NetworkManager networkManager;

    private int currIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();


        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    void Update()
    {
        
    }

    public void SetPrevGame()
    {

    }

    public void SetNextGame()
    {

    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void LeaveRoom()
    {
        networkManager.LeaveRoom();
    }
}
