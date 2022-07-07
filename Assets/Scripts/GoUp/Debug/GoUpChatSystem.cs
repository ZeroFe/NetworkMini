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

    void Start()
    {
        // 초기 채팅창 활성화
    }

    void Update()
    {
        // 채팅 창에 무언가 적었고 엔터키를 누른다면
        if ((chattingScroll.chatInputField.text.Length > 0) && (Input.GetKeyDown(KeyCode.Return)))
        {
            // 답을 적을 수 있는 상태이고 이번 라운드에 처음 답하는거면 답 비교하기
            if (GoUpGameSystem.Instance.IsAnswerable)
            {
                // 비교한 결과에 따라 결과 적용하기
                GoUpGameSystem.Instance.JudgeAnswer(IsCorrectAnswer());
            }

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
