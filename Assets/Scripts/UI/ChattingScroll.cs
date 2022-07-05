using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChattingScroll : MonoBehaviour
{
    public TMP_InputField chatInputField;
    public ScrollRect chatScrollView;
    public TextMeshProUGUI chatText;

    private RectTransform scrollRectTr;
    private RectTransform chatTextTr;

    private float originHeight;
    private bool isChatting = false;

    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        chatTextTr = chatText.GetComponent<RectTransform>();
        scrollRectTr = chatScrollView.GetComponent<RectTransform>();
    }

    private void Start()
    {
        // 스크롤 사이즈 맞추기
        //var chatScroll = chatScrollView.GetComponent<RectTransform>();
        //var newRectSize = new Vector2(chatContent.sizeDelta.x, chatScroll.sizeDelta.y);
        //chatContent.sizeDelta = newRectSize;
        //originHeight = newRectSize.y;
        //print(chatContent.anchoredPosition.y);
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        // 처음 엔터를 누르면 Input Field 활성화
        // Input Field 활성화된 상태에서 엔터를 누르면 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // 초기엔 채팅을 치는 상태가 아니다
            if (!isChatting)
            {
                // 채팅 활성화
                chatInputField.Select();
            }
            // InputField에 채팅을 치는 상태면 엔터 누르면 입력을 마친다
            else
            {
                Chat("Player : " + chatInputField.text);
                chatInputField.text = "";
                chatInputField.Select();
            }

            // 반대로 전환
            isChatting = !isChatting;
        }
    }

    public void ScrollUp()
    {

    }

    public void ScrollDown()
    {

    }

    public void Chat(string message)
    {
        chatText.text += message + "\n";

        // 스크롤 버튼 활성화 비활성화 결정

    }
}
