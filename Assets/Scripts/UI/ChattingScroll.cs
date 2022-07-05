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
        // ��ũ�� ������ ���߱�
        var chatScroll = chatScrollView.GetComponent<RectTransform>();
        var newRectSize = new Vector2(chatContent.sizeDelta.x, chatScroll.sizeDelta.y);
        chatContent.sizeDelta = newRectSize;
        originHeight = newRectSize.y;
        print(chatContent.anchoredPosition.y);
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

    public void Chat(string message)
    {
        var chatText = Instantiate(chatTextPrefab);
        chatText.transform.SetParent(chatContent);
        // ä�� ������Ʈ�� ���� �������� �����Ǵ� ������ �־� localScale�� ������ 1�� ����
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
