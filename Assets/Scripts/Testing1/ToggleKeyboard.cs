using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class ToggleKeyboard : UIButton
{

    public int current;

    GameObject keyboard0;
    GameObject keyboard1;
    GameObject keyboard2;
    Vector3 Active;
    Vector3 Inactive;

    // Start is called before the first frame update
    void Start()
    {

        Initialize();
        current = 0; 
        keyboard0 = GameObject.Find("Condition0");
        keyboard1 = GameObject.Find("Condition1");
        keyboard2 = GameObject.Find("Condition2");

        Active = new Vector3(.5f, 1, .2f);
        Inactive = new Vector3(-.6f, -.9f, 2.9f);

    }

    // Update is called once per frame
    public override void ActionEvent()
    {
        current = current + 1;
        if (current == 3) current = 0;

        if (current == 0)
        {
            keyboard0.transform.position = Active;
            keyboard1.transform.position = Inactive;
            keyboard2.transform.position = Inactive;
        }

        else if (current == 1)
        {
            keyboard0.transform.position = Inactive;
            keyboard1.transform.position = Active;
            keyboard2.transform.position = Inactive;
        }
        else if (current == 2)
        {
            keyboard0.transform.position = Inactive;
            keyboard1.transform.position = Inactive;
            keyboard2.transform.position = Active;
        }

    }
}
