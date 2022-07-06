using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GoUpChatSystem : MonoBehaviourPun
{
    // �÷��̾��� �Է� �޾ƿ��� 
    public ChattingScroll chattingScroll;

    void Start()
    {
        // �ʱ� ä��â Ȱ��ȭ
    }

    void Update()
    {
        // ä�� â�� ���� ������ ����Ű�� �����ٸ�
        if ((chattingScroll.chatInputField.text.Length > 0) && (Input.GetKeyDown(KeyCode.Return)))
        {
            // ���� ���� �� �ִ� �����̰� �̹� ���忡 ó�� ���ϴ°Ÿ� �� ���ϱ�
            if (GoUpGameSystem.Instance.IsAnswerable)
            {
                // ���� ����� ���� ��� �����ϱ�
                GoUpGameSystem.Instance.JudgeAnswer(IsCorrectAnswer());
            }

            // ä�� ��� : ä���� ����� ������� ����ؾ��Ѵ�
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
        // ���� ��������
        string memo = GoUpGameSystem.Instance.GetMemo();
        // �Է°� �ް�
        string answer = chattingScroll.chatInputField.text;
        
        // ��
        return answer == memo;
    }
}
