using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


// 이건 플레이어에 붙여야겠다


public class InputTextt_YJ : MonoBehaviour
{
    //test Time
    private float time;

    // 자식오브젝트의 텍스트 
    private Text childTextForNull;

    
    // 플레이어의 입력 받아오기 
    public InputField answerInput;
    public string answer = null;

    // 답지_파일 받아오기
    public FileStream answerFile;
    public string memoPath;

    // 답지_줄 받아오기
    public string memo;

    // 라운드
    public int round = 0;

    // 시간
    private int firsttime = 18;
    private int roundtime = 10;

    // 싱글톤 만들기
    public static InputTextt_YJ instance;
    private void Awake()
    {
        InputTextt_YJ.instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 14초 있다가 시작
        yield return new WaitForSeconds(firsttime);

        // 10초마다 round 올려주기 -> 5초는 UI, 3초는 8쉬기, 2초는 문제이름
        for (int i = 0; i < 9; i++)
        {
            round++;
            yield return new WaitForSeconds(roundtime);


        }

    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        print(time);
        
            // 답가져와서
            GetMeMo();
            // 입력값 받아서 
            GetAnswer();

            // 답변을 입력한 상태고 && 엔터키를 누른다면
            if ((answer.Length > 0) && (Input.GetKeyDown(KeyCode.Return)))
            {
                // 비교해주기
                CompareAnswer();

        }

    }

    public void GetMeMo()
    { 
        // 파일 받아오기
        memoPath = Application.dataPath;
        memoPath += "/Text/QuestionStorage.txt";

        // contents에다가 전체 파일 가져와서 contents라는 변수에 저장하기
        string[] contents = System.IO.File.ReadAllLines(memoPath);

        // contents에 무언가가 저장이 되었다면
        if (contents.Length > 0)
        {
            // 지금 라운드와 같은 순서로 읽어주기
            memo = contents[round];

        }


    }
    public void GetAnswer()
    {
        // memo랑 입력받은거랑 비교하기
        // 입력한거는 answer 
        // 답지는 memo
        answer = answerInput.GetComponent<InputField>().text;



    }
    public void CompareAnswer()
    {

        print("입력한 값 : "+ answer);
        print("memo 값 : " + memo);
        // 정답이면 
        if (answer == memo)
        {
            // 올려주기 
            this.gameObject.transform.position += new Vector3(0, 1.0f, 0); 
        }

        // 오답이면 
        else if (answer != memo )
        {
            this.gameObject.transform.position += new Vector3(0, 0, 0);

        }

    }

}
