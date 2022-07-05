using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_2b_YJ : MonoBehaviour
{
    private float time;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = this.gameObject.transform.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 9.0f)
        {
            image.enabled = true;
        }

        if (time >= 12.0f)
        {
            this.gameObject.SetActive(false);
        }

    }
}
