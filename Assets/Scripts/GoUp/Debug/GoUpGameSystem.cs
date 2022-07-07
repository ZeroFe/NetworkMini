using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

/// <summary>
/// ���� ������ �а� UI�� ������ �� ��� �� �����ϴ� Ŭ����
/// </summary>
public class GoUpGameSystem : MonoBehaviourPun
{
    public static GoUpGameSystem Instance { get; private set; }

    [Header("Title")]
    public Image titleImage;
    [SerializeField] private float titleAnimTime = 5.0f;

    [Header("Memo UI")]
    public TextMeshProUGUI memoText;
    public Image mask;

    [Header("Guide")]
    public string[] guides;
    public float guideWaitTime = 3.0f;

    // ����_���� �޾ƿ���
    private readonly string MEMO_PATH = "/Text/QuestionStorage.txt";
    private string[] memoLines;

    [Header("Round")]
    public int MaxRound = 9;
    public int Round { get; private set; } = 0;

    // ������ȣ ģ����
    private readonly string NUMBER_TEXT = "����";

    // �亯 ���� ����
    // �亯 ������ �������� Ȯ�� - ó�� ���ѰŸ� �亯 ó��
    public bool IsAnswerable { get; private set; } = false;
    // ���� ����
    private int currentRank = 0;

    // �� ĳ���� �� ��� ĳ���͵� ����
    public GoUpPlayer myPlayer;
    public List<GoUpPlayer> players = new List<GoUpPlayer>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ReadAnswerFile();
    }

    /// <summary>
    /// ���Ϸκ��� ���� ���� �޾ƿ���
    /// </summary>
    private void ReadAnswerFile()
    {
        // ���� ��� ����
        string answerPath = Application.dataPath + MEMO_PATH;
        // ���� ������ �о ���� �ٿ� �ֱ�
        memoLines = System.IO.File.ReadAllLines(answerPath);
    }
    
    public void StartGame()
    {
        photonView.RPC(nameof(StartGameRPC), RpcTarget.All);
    }

    [PunRPC]
    private void StartGameRPC()
    {
        StartCoroutine(IEStartGuide());
    }

    IEnumerator IEStartGuide()
    {
        // Title Animation �����ְ�
        yield return IETitleAnimation();

        // ���� ������ �����ش�
        for (int i = 0; i < guides.Length; i++)
        {
            memoText.text = guides[i];
            // �ѹ� ����� ������ 3�� ��ٸ���
            yield return new WaitForSeconds(guideWaitTime);
        }
        
        // ���۹��� ģ���� �� �ϰ� ���� ��μ� ����
        StartCoroutine(IEStartRounds());
    }

    IEnumerator IETitleAnimation()
    {
        titleImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(titleAnimTime);
        titleImage.gameObject.SetActive(false);
    }

    // �ٿ� ����� �Ѿ��
    IEnumerator IEStartRounds()
    {
        // �ȳ���������
        for (Round = 0; Round < MaxRound; Round++)
        {
            // ��ȣ ���ֱ�
            ShowNumber();
            // 2�� �ڿ� �ٲٰ�
            yield return new WaitForSeconds(2);

            ShowMemo();
            // 5�� �ڿ� ���ְ�
            yield return new WaitForSeconds(5);

            TurnOffMemo();
            // 3�ʵڿ� 
            yield return new WaitForSeconds(3);

            // ���� ���� ����
        }

        // ��� â �����ֱ�
        ShowResult();
    }

    private void ShowNumber()
    {
        // ����ũ ���ֱ� 
        mask.enabled = true;

        // �޸� ���ֱ� 
        memoText.enabled = true;
        memoText.text = NUMBER_TEXT + (Round + 1);
    }

    /// <summary>
    /// �޸� �����ֱ�
    /// �� ������ ������ ���� �� �ְ�, ģ ä���� �������� �����ȴ�
    /// </summary>
    private void ShowMemo()
    {
        currentRank = 0;
        IsAnswerable = true;

        // �޾ƿ� �޸� text�� ���ش�
        memoText.text = memoLines[Round];
    }

    private void TurnOffMemo()
    {
        IsAnswerable = false;

        // �޸� �������� �� �̻� ������ ���� �� ����
        // ���� ������ ���� ���� ���¶�� Ʋ�ȴٰ� �����ؾ� �Ѵ�
        if (IsAnswerable)
        {
            IsAnswerable = false;
            JudgeAnswer(false);
        }

        // ����ũ�� memo ���ֱ�
        mask.enabled = false;
        memoText.enabled = false;
    }

    private void ShowResult()
    {
        // ������ �� ���â �����ֱ�
        print("End");
    }

    public string GetMemo()
    {
        Debug.Assert(memoLines.Length > 0, "Error : answerLines is empty");
        return memoLines[Round];
    }

    // ���� ����
    public void JudgeAnswer(bool correct)
    {
        // �����̸� �� ĳ���� �ø���
        if (correct)
        {
            print("Correct");
            // �÷��ֱ� 
            //myPlayer.GoUp();
        }
        // �ƴϸ� �� ĳ���� ������
        else
        {
            print("Fail");
            //myPlayer.GoDown();
        }

        // ������ �޾����� �´��� Ʋ������ ������� �� �̻� ������ ���� ���ϰ� ����
        IsAnswerable = false;
    }

    // RPC - ���� ó�� : 
    // RPC - ���� ó�� : 
}
