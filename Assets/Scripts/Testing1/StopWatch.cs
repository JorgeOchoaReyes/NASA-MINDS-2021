using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopWatch : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float theTime;
    public float speed = 1;
    public bool playing;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing == true)
        {
            theTime += Time.deltaTime * speed;
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            string milliseconds =  ((theTime * 1000f) % 1000).ToString("00");
            text.text = minutes + ":" + seconds + ":" + milliseconds ;
        }
    }

    public void clickPlay()
    {
        playing = true; 
    }

    public void clickReset()
    {
        theTime = 0;
        string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        string milliseconds = ((theTime * 1000f) % 1000).ToString("00");
        text.text = minutes + ":" + seconds + ":" + milliseconds;
    }

    public void clickPause()
    {
        playing = false; 
    }

}
