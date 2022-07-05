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

    // ����_�� �޾ƿ���
    public string memo;

    // ����
    public int round = 0;

    // �ð�
    private int firsttime = 18;
    private int roundtime = 10;

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

        }

    }

    public void GetMeMo()
    { 
        // ���� �޾ƿ���
        memoPath = Application.dataPath;
        memoPath += "/Text/QuestionStorage.txt";

        // contents���ٰ� ��ü ���� �����ͼ� contents��� ������ �����ϱ�
        string[] contents = System.IO.File.ReadAllLines(memoPath);

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

        print("�Է��� �� : "+ answer);
        print("memo �� : " + memo);
        // �����̸� 
        if (answer == memo)
        {
            // �÷��ֱ� 
            this.gameObject.transform.position += new Vector3(0, 1.0f, 0); 
        }

        // �����̸� 
        else if (answer != memo )
        {
            this.gameObject.transform.position += new Vector3(0, 0, 0);

        }

    }

}
