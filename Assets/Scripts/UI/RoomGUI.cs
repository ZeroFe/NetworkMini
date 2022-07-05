using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoomGUI : MonoBehaviour
{
    public TextMeshProUGUI roomName;

    [Header("Game Explain")] 
    public Image gameScreenShot;
    public TextMeshProUGUI gameName;
    public TextMeshProUGUI gameExplain;


    public GameData[] gameDatas;


    private NetworkManager networkManager;

    private int currIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        UpdateGameExplain();
    }

    void Update()
    {
        
    }

    public void SetPrevGame()
    {
        if (--currIndex < 0)
        {
            currIndex += gameDatas.Length;
        }
        UpdateGameExplain();
    }

    public void SetNextGame()
    {
        currIndex = (currIndex + 1) % gameDatas.Length;
        UpdateGameExplain();
    }

    private void UpdateGameExplain()
    {
        print("Update Game Explain");
        var gameData = gameDatas[currIndex];

        // 이후 애니메이션 추가
        gameScreenShot.sprite = gameData.gameScreenShot;
        gameName.text = gameData.gameName;
        gameExplain.text = gameData.gameExplain;
    }

    public void StartGame()
    {
        var gameNumber = currIndex != 0 ? currIndex : UnityEngine.Random.Range(1, gameDatas.Length);
        var gameData = gameDatas[gameNumber];
        PhotonNetwork.LoadLevel(gameData.gameScene.name);
    }

    public void LeaveRoom()
    {
        networkManager.LeaveRoom();
    }
}
