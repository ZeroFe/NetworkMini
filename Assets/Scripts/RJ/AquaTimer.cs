using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AquaTimer : MonoBehaviour
{
    float time;
    float totalTime;
    public Image timeSlider;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 70;
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        //totalTimeSlider.value = (totalTime - time) / totalTime;
        timeSlider.fillAmount = (totalTime - time)/totalTime;
    }
    
    void GameOver()
    {
        if (time <= 0)
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
