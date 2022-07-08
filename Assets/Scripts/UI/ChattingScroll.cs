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

    public bool autoActive = true;
    public bool Active { get; set; } = false;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        // �ڵ� Ȱ��ȭ
        chatInputField.ActivateInputField();
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        // ó�� ���͸� ������ Input Field Ȱ��ȭ
        // Input Field Ȱ��ȭ�� ���¿��� ���͸� ������ 
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //}
        //chatInputField.ActivateInputField();
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

        // �ڵ� Ȱ��ȭ ��Ȱ��ȭ ����

    }

    public void Clear()
    {
        chatInputField.text = "";
    }
}
