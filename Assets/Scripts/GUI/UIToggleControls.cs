using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class UIToggleControls : UIButton
{

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        SetLabel(2);
    }

    void SetLabel(int b)
    {
        if (SceneManager.GetActiveScene().name == "Testing1")
        {
            GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>().thickness = 0.002f;
            GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>().thickness = 0.002f;
        }

        if (b == 0)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Toggle controls\n(Hands on + Trigger)";
        }
        else if (b == 1)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Toggle controls\n(Finger Touch)";
        }
        else if (b == 2)
        {
            if (SceneManager.GetActiveScene().name == "Testing1")
            {
                GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>().thickness = 0;
                GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>().thickness = 0;
            }
            GetComponentInChildren<TextMeshProUGUI>().text = "Toggle controls\n(Head + Trigger)";
        }
        else if (b == 3)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Toggle controls\n(Head + Dwell Time)";
        }
                
    }  

    // Update is called once per frame
    public override void ActionEvent()
    {
        // For each button, change the control type
        GameObject[] go;
        go = GameObject.FindGameObjectsWithTag("UIButton");

        for (int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<UIButton>().ToggleControls();
        }

        SetLabel(go[0].GetComponent<UIButton>().controlType);
    }
}

