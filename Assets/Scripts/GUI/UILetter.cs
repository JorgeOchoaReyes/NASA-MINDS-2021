using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class UILetter : UIButton
{
    private int letter;
    public int keyboard_type;

    void Start()
    {
        Initialize();
    }

    public void SetLetter(int i,string str)
    {
        letter = i;
        
        GetComponentInChildren<TextMeshProUGUI>().text = str;
    }

    

    public override void ActionEvent()
    {
        if (keyboard_type == 0)
            VirtualKeyboard0.PressKey(letter);
        else if (keyboard_type == 1)
            VirtualKeyboard1.PressKey(letter);
        else if (keyboard_type == 2)
        {
            if(VirtualKeyboard3.level == 1 && transform.name.Length > 9)
            {
                VirtualKeyboard3.PopUpHide();
                VirtualKeyboard3.SetLevel0(); 
                //if you want popup to hide on each level change and only activate on a reclick add => return;
            }
            VirtualKeyboard3.PressKey(letter);
        }
    }
}
