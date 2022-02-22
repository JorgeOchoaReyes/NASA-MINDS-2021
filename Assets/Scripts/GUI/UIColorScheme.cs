using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class UIColorScheme : UIButton
{
    public int visible;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        visible = 0;
    }

    public override void ActionEvent()
    {
        GUI.SetNextColorScheme();
        // Updates the color of the GUI
        GameObject.Find("Canvas").GetComponent<UICanvas>().SetGUIColorScheme();
    }
}
