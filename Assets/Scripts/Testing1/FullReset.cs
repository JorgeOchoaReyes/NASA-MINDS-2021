using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class FullReset : UIButton
{

    StopWatch timerTesting;
    Text logger; 



    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        timerTesting = GameObject.FindGameObjectWithTag("Timer").GetComponent<StopWatch>();
        
    }

    public void Clear_All()
    {
        VirtualKeyboard0.Clear();
        VirtualKeyboard1.Clear();
        VirtualKeyboard3.Clear();
        timerTesting.playing = false;
        timerTesting.clickReset();
        logger = GameObject.Find("UserInputCheck").GetComponent<Text>();
        logger.text = "";
    }

    // Update is called once per frame
    public override void ActionEvent()
    {
        Debug.Log("Clicked Reset!");
        Clear_All();
    }
}
