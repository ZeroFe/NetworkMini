using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

/// <summary>
/// 정답 정보를 읽고 UI로 보내는 등 모든 걸 관리하는 클래스
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

    // 답지_파일 받아오기
    private readonly string MEMO_PATH = "/Text/QuestionStorage.txt";
    private string[] memoLines;

    [Header("Round")]
    public int MaxRound = 9;
    public int Round { get; private set; } = 0;

    // 문제번호 친구들
    private readonly string NUMBER_TEXT = "문제";

    // 답변 판정 관련
    // 답변 가능한 상태인지 확인 - 처음 답한거면 답변 처리
    public bool IsAnswerable { get; private set; } = false;
    // 현재 순위
    private int currentRank = 0;

    // 내 캐릭터 및 상대 캐릭터들 저장
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
    /// 파일로부터 정답 문장 받아오기
    /// </summary>
    private void ReadAnswerFile()
    {
        // 파일 경로 설정
        string answerPath = Application.dataPath + MEMO_PATH;
        // 정답 파일을 읽어서 정답 줄에 넣기
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
        // Title Animation 보여주고
        yield return IETitleAnimation();

        // 게임 설명을 보여준다
        for (int i = 0; i < guides.Length; i++)
        {
            memoText.text = guides[i];
            // 한번 띄워준 다음에 3초 기다린다
            yield return new WaitForSeconds(guideWaitTime);
        }
        
        // 시작문구 친구들 다 하고 나면 비로소 시작
        StartCoroutine(IEStartRounds());
    }

    IEnumerator IETitleAnimation()
    {
        titleImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(titleAnimTime);
        titleImage.gameObject.SetActive(false);
    }

    // 다움 라운드로 넘어가기
    IEnumerator IEStartRounds()
    {
        // 안끝났을때는
        for (Round = 0; Round < MaxRound; Round++)
        {
            // 번호 켜주기
            ShowNumber();
            // 2초 뒤에 바꾸고
            yield return new WaitForSeconds(2);

            ShowMemo();
            // 5초 뒤에 꺼주고
            yield return new WaitForSeconds(5);

            TurnOffMemo();
            // 3초뒤에 
            yield return new WaitForSeconds(3);

            // 다음 라운드 진행
        }

        // 결과 창 보여주기
        ShowResult();
    }

    private void ShowNumber()
    {
        // 마스크 켜주기 
        mask.enabled = true;

        // 메모 켜주기 
        memoText.enabled = true;
        memoText.text = NUMBER_TEXT + (Round + 1);
    }

    /// <summary>
    /// 메모 보여주기
    /// 이 때부터 정답을 적을 수 있고, 친 채팅이 정답으로 판정된다
    /// </summary>
    private void ShowMemo()
    {
        currentRank = 0;
        IsAnswerable = true;

        // 받아온 메모를 text에 해준다
        memoText.text = memoLines[Round];
    }

    private void TurnOffMemo()
    {
        IsAnswerable = false;

        // 메모가 꺼졌으니 더 이상 정답을 적을 수 없다
        // 만약 정답을 적지 않은 상태라면 틀렸다고 판정해야 한다
        if (IsAnswerable)
        {
            IsAnswerable = false;
            JudgeAnswer(false);
        }

        // 마스크랑 memo 꺼주기
        mask.enabled = false;
        memoText.enabled = false;
    }

    private void ShowResult()
    {
        // 끝났을 때 결과창 보여주기
        print("End");
    }

    public string GetMemo()
    {
        Debug.Assert(memoLines.Length > 0, "Error : answerLines is empty");
        return memoLines[Round];
    }

    // 정답 판정
    public void JudgeAnswer(bool correct)
    {
        // 정답이면 내 캐릭터 올리기
        if (correct)
        {
            print("Correct");
            // 올려주기 
            //myPlayer.GoUp();
        }
        // 아니면 내 캐릭터 내리기
        else
        {
            print("Fail");
            //myPlayer.GoDown();
        }

        // 정답을 받았으면 맞는지 틀린지와 관계없이 더 이상 정답을 받지 못하게 막음
        IsAnswerable = false;
    }

    // RPC - 정답 처리 : 
    // RPC - 실패 처리 : 
}
