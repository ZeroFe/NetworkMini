using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AquaTimer : MonoBehaviour
{
    public static AquaTimer Instance { get; private set; }

    [Header("Title")]
    public GameObject titleImage;
    public float titleShowTime = 3.0f;

    [Header("Ready")]
    public GameObject readyObjects;
    public TextMeshProUGUI readyTimeText;
    public int readyTime = 10;

    [Header("Game")]
    public GameObject minimapImage;
    public GameObject timerImage;

    public bool IsShootable { get; private set; } = false;

    float time;
    float totalTime;
    public Image timeSlider;

    WaitForSeconds ws = new WaitForSeconds(0.1f);

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        totalTime = 70;

        yield return IEShowTitle();
        yield return IEReady();

        IsShootable = true;
        minimapImage.SetActive(true);
        timerImage.SetActive(true);

        while (time < totalTime)
        {
            yield return ws;
            time += 0.1f;
            timeSlider.fillAmount = (totalTime - time) / totalTime;
        }
        GameOver();
    }

    IEnumerator IEShowTitle()
    {
        titleImage.SetActive(true);
        yield return new WaitForSeconds(titleShowTime);
        titleImage.SetActive(false);
    }

    IEnumerator IEReady()
    {
        readyObjects.SetActive(true);
        readyTimeText.text = readyTime.ToString();
        while (readyTime > 0)
        {
            yield return new WaitForSeconds(1);
            readyTime--;
            readyTimeText.text = readyTime.ToString();
        }
        readyObjects.SetActive(false);
    }
    
    void GameOver()
    {
        if (time <= 0)
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
