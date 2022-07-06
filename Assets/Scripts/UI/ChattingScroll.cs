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
        // ��ũ�� ������ ���߱�
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
        // ó�� ���͸� ������ Input Field Ȱ��ȭ
        // Input Field Ȱ��ȭ�� ���¿��� ���͸� ������ 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // �ʱ⿣ ä���� ġ�� ���°� �ƴϴ�
            if (!isChatting)
            {
                // ä�� Ȱ��ȭ
                chatInputField.Select();
            }
            // InputField�� ä���� ġ�� ���¸� ���� ������ �Է��� ��ģ��
            else
            {
                Chat("Player : " + chatInputField.text);
                chatInputField.text = "";
                chatInputField.Select();
            }

            // �ݴ�� ��ȯ
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

        // ��ũ�� ��ư Ȱ��ȭ ��Ȱ��ȭ ����

    }
}
