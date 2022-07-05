using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChattingScroll : MonoBehaviour
{
    public TMP_InputField chatInputField;
    public ScrollRect chatScrollView;
    public RectTransform chatContent;
    public GameObject chatTextPrefab;

    private float originHeight;
    private bool isChatting = false;

    private void Start()
    {
        // 스크롤 사이즈 맞추기
        var chatScroll = chatScrollView.GetComponent<RectTransform>();
        var newRectSize = new Vector2(chatContent.sizeDelta.x, chatScroll.sizeDelta.y);
        chatContent.sizeDelta = newRectSize;
        originHeight = newRectSize.y;
        print(chatContent.anchoredPosition.y);
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

    public void Chat(string message)
    {
        var chatText = Instantiate(chatTextPrefab);
        chatText.transform.SetParent(chatContent);
        // 채팅 오브젝트의 로컬 스케일이 변동되는 문제가 있어 localScale을 강제로 1로 변경
        chatText.transform.localScale = Vector3.one;
        chatText.GetComponent<Text>().text = message;

        var msg = chatText.GetComponent<RectTransform>();

        var newHeight = Mathf.Max(originHeight, chatContent.childCount * msg.sizeDelta.y);
        var newRectSize = new Vector2(chatContent.sizeDelta.x, newHeight);
        chatContent.sizeDelta = newRectSize;
        var newPosY = Mathf.Max(0, newHeight - originHeight);
        chatContent.anchoredPosition = new Vector2(chatContent.anchoredPosition.x, newPosY);
    }
}
