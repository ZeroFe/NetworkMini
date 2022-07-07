using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GoUpChatSystem : MonoBehaviourPun
{
    // 플레이어의 입력 받아오기 
    public ChattingScroll chattingScroll;
    public GoUpPlayer[] players;

    void Start()
    {
        // 초기 채팅창 활성화
    }

    void Update()
    {
        // 채팅 창에 무언가 적었고 엔터키를 누른다면
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 문장이 비어있는 경우는 무시
            if (chattingScroll.chatInputField.text.Length == 0)
            {
                return;
            }

            // 답을 적을 수 있는 상태이고 이번 라운드에 처음 답하는거면 답 비교하기
            if (GoUpGameSystem.Instance.IsAnswerable)
            {
                // 비교한 결과에 따라 결과 적용하기
                GoUpGameSystem.Instance.JudgeAnswer(IsCorrectAnswer());
            }

            // 플레이어 채팅 띄우기
            PlayerChat(chattingScroll.chatInputField.text);
            // 채팅 출력 : 채팅은 정답과 상관없이 출력해야한다
            Chat(chattingScroll.chatInputField.text);
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

    public void PlayerChat(string message)
    {
        photonView.RPC(nameof(PlayerChatRPC), RpcTarget.All,
            PhotonNetwork.LocalPlayer.ActorNumber - 1,
            message);
    }

    [PunRPC]
    public void PlayerChatRPC(int playerNum, string message)
    {
        players[playerNum].ShowChat(message);
    }

    private bool IsCorrectAnswer()
    {
        // 정답 가져오기
        string memo = GoUpGameSystem.Instance.GetMemo();
        // 입력값 받고
        string answer = chattingScroll.chatInputField.text;
        
        // 비교
        return answer == memo;
    }
}
