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
        // 자동 활성화
        chatInputField.ActivateInputField();
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        // 처음 엔터를 누르면 Input Field 활성화
        // Input Field 활성화된 상태에서 엔터를 누르면 
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
        

        // 스크롤 버튼 활성화 비활성화 결정

        // 자동 활성화 비활성화 결정

    }

    public void Clear()
    {
        chatInputField.text = "";
    }
}
