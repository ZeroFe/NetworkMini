using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


// �̰� �÷��̾ �ٿ��߰ڴ�


public class InputTextt_YJ : MonoBehaviour
{
    //test Time
    private float time;

    // �ڽĿ�����Ʈ�� �ؽ�Ʈ 
    private Text childTextForNull;

    
    // �÷��̾��� �Է� �޾ƿ��� 
    public InputField answerInput;
    public string answer = null;

    // ����_���� �޾ƿ���
    public FileStream answerFile;
    public string memoPath;
    public string[] contents;

    // ����_�� �޾ƿ���
    public string memo;

    // ����
    public int round = 0;

    // �ð�
    private int firsttime = 18;
    private int roundtime = 10;

    // ����üũ
    public int correct;

    // ���üũ�� ���䰹��
    public int correctNumber = 0;

    // �̵�
    public float speed;
    // �̱��� �����
    public static InputTextt_YJ instance;
    private void Awake()
    {
        InputTextt_YJ.instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 14�� �ִٰ� ����
        yield return new WaitForSeconds(firsttime);

        // 10�ʸ��� round �÷��ֱ� -> 5�ʴ� UI, 3�ʴ� 8����, 2�ʴ� �����̸�
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
        
            // �䰡���ͼ�
            GetMeMo();
            // �Է°� �޾Ƽ� 
            GetAnswer();

            // �亯�� �Է��� ���°� && ����Ű�� �����ٸ�
            if ((answer.Length > 0) && (Input.GetKeyDown(KeyCode.Return)))
            {
                // �����ֱ�
                CompareAnswer();

                // ��� ����
                IfAnswerGoUp();
                IfWrongGoDown();



             }

    }

    public void GetMeMo()
    { 
        // ���� �޾ƿ���
        memoPath = Application.dataPath;
        memoPath += "/Text/QuestionStorage.txt";

        // contents���ٰ� ��ü ���� �����ͼ� contents��� ������ �����ϱ�
        contents = System.IO.File.ReadAllLines(memoPath);

        // contents�� ���𰡰� ������ �Ǿ��ٸ�
        if (contents.Length > 0)
        {
            // ���� ����� ���� ������ �о��ֱ�
            memo = contents[round];

        }


    }
    public void GetAnswer()
    {
        // memo�� �Է¹����Ŷ� ���ϱ�
        // �Է��ѰŴ� answer 
        // ������ memo
        answer = answerInput.GetComponent<InputField>().text;



    }
    public void CompareAnswer()
    {

        // �����̸� 
        if (answer == memo)
        {
            // �����̶��ϴ�
            correct = 1;
        }

        // �����̸� 
        else if (answer != memo )
        {
            // �����̶��ϴ�
            correct = -1;
        }

    }
    public void IfAnswerGoUp()
    {
        // �����̸�
        if ( correct == 1 )
        {
            // �÷��ֱ� 
            // this.gameObject.transform.position += Vector3.up;
            this.gameObject.transform.position += Vector3.up * speed;


            correctNumber += 1;



        }

        correct = 0;

    }
    public void IfWrongGoDown()
    {
        // �����̸�
        if (correct == -1)
        {
            // �����ֱ�
            this.gameObject.transform.position += Vector3.down * speed;


        }

        correct = 0;


    }
}
