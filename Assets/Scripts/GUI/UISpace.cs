using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpace : UIButton
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
        {
            VirtualKeyboard0.Space();
        }
           
        else if (keyboard_type == 1)
        {
            VirtualKeyboard1.Space();
        }
           
        else if (keyboard_type == 2)
        {
            VirtualKeyboard3.Space();
        }
            
    }
}
