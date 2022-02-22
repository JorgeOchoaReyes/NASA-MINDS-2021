using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class UIClearOutput : UIButton
{
    // Start is called before the first frame update
    public int keyboard_type;
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    public override void ActionEvent()
    {
        if (keyboard_type == 0)
            VirtualKeyboard0.Clear();
        else if (keyboard_type == 1)
            VirtualKeyboard1.Clear();
        else if (keyboard_type == 2)
            VirtualKeyboard3.Clear(); 
    }
}
