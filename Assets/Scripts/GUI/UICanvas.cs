using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Globalization;
using Valve.VR;
using Valve.VR.Extras;

public class UICanvas : MonoBehaviour
{

    public void SetGUIColorScheme()
    {
        GameObject[] go;
        go = GameObject.FindGameObjectsWithTag("UIButton");
        for (int i = 0; i < go.Length; i++)
        {
            ColorBlock colorBlock = go[i].GetComponent<Button>().colors;
            colorBlock.normalColor = GUI.SetNormalColorUnSelected();
            colorBlock.highlightedColor = GUI.SetNormalColorUnSelected();
            go[i].GetComponent<Button>().colors = colorBlock;
            go[i].GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorUnSelectedButton();

            GameObject quad = GameObject.Find(go[i].name + "_quad");
            if (quad)
                quad.GetComponent<Renderer>().material.color = GUI.SetFontColorUnSelectedButton();
        }
        go = GameObject.FindGameObjectsWithTag("UILabel");
        for (int i = 0; i < go.Length; i++)
        {
            ColorBlock colorBlock = go[i].GetComponent<Button>().colors;
            colorBlock.normalColor = GUI.SetBGLabelColor();
            colorBlock.highlightedColor = GUI.SetBGLabelColor();
            go[i].GetComponent<Button>().colors = colorBlock;
            go[i].GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorLabel();

            GameObject quad = GameObject.Find(go[i].name + "_quad");
            if (quad)
                quad.GetComponent<Renderer>().material.color = GUI.SetFontColorUnSelectedButton();
        }
        go = GameObject.FindGameObjectsWithTag("UIValue");
        for (int i = 0; i < go.Length; i++)
        {
            ColorBlock colorBlock = go[i].GetComponent<Button>().colors;
            colorBlock.normalColor = GUI.SetBGValueColor();
            colorBlock.highlightedColor = GUI.SetBGValueColor();
            go[i].GetComponent<Button>().colors = colorBlock;
            go[i].GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorValue();
        }


        go = GameObject.FindGameObjectsWithTag("UIQuad");
        for (int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<Renderer>().material.color = GUI.SetBGValueColor();
        }

        // Update the colors of the LEFT pointer
        GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>().clickColor = new Color(0.8F, 0.4F, 0.1F, 1.0F);
        //    GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>().unselectedColor = new Color(0.8F, 0.8F, 0.1F, 1.0F);
        //    GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>().selectedColor = new Color(0.4F, 0.8F, 0.1F, 1.0F);

        // Update the colors of the RIGHT pointer
        GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>().clickColor = new Color(0.8F, 0.4F, 0.1F, 1.0F);
        //    GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>().unselectedColor = new Color(0.8F, 0.8F, 0.1F, 1.0F);
        //    GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>().selectedColor = new Color(0.4F, 0.8F, 0.1F, 1.0F);

       
        // Update the name of the Color Scheme in the UI
        GameObject go1 = GameObject.Find("ButtonColorScheme");
        if (go1)
            go1.GetComponentInChildren<TextMeshProUGUI>().text = " UI " + GUI.GetColorSchemeName();
    }

    private void Awake()
    {
        GUI.LoadColorScheme();
    }

    // Start is called before the first frame update
    void Start()
    {
        GUI.ColorScheme = 0;
        // Set the appropriate GUI colors
        SetGUIColorScheme();
    }   

    // Update is called once per frame
    void Update()
    {
        // To leave properly the program
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("gone");
            Application.Quit();
        } 
    }
}


