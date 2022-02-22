using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class ClearLogger : UIButton
{

    StopWatch timerTest;
    public Text logger; 

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        timerTest = GameObject.Find("TimeText").GetComponent<StopWatch>();
    }




    // Update is called once per frame
    public override void ActionEvent()
    {
          logger = GameObject.Find("UserInputCheck").GetComponent<Text>();
          logger.text = "";
    }
}
