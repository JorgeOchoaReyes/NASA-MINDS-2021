using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public static class VirtualKeyboard1
{

    // List of functions for the virtual keyboard
    public static GameObject goInput;
    public static GameObject goOutput;
    public static GameObject[] gokeys; // list of 

    public static string[][] BlockUpper;
    public static string[][] BlockLower;

    private static GameObject[] GOBlock;

    public static bool state;

    public static string OutputText;
    public static string InputText;

    public static int level;// 0 main level in the tree, 1 second level in the tree
    public static int child;

    public static StopWatch timer;
    public static Text logger;

    public static bool IsLowerCase()
    {
        return !state;
    }

    public static bool IsUpperCase()
    {
        return state;
    }

    public static void SetInputOutputText(string str)
    {
        InputText = str;
        OutputText = "";
        goInput = GameObject.Find("ButtonInputText1");
        goOutput = GameObject.Find("ButtonOutputText1");
        goInput.GetComponentInChildren<TextMeshProUGUI>().text = InputText;
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }


    public static string GetButtonString(string[] strv)
    {
        return strv[0] + " " + strv[1] + "\n" + 
            strv[2] + " " + strv[3] + " " + strv[4] + "\n" + 
            strv[5] + " " + strv[6];
    }

    public static void SetKeys()
    {   
        level = 0;
        state = false;
        // create the array
        GOBlock = new GameObject[7];
        GOBlock[0] = GameObject.Find("ButtonBlock0");
        GOBlock[1] = GameObject.Find("ButtonBlock1");
        GOBlock[2] = GameObject.Find("ButtonBlock2");
        GOBlock[3] = GameObject.Find("ButtonBlock3");
        GOBlock[4] = GameObject.Find("ButtonBlock4");
        GOBlock[5] = GameObject.Find("ButtonBlock5");
        GOBlock[6] = GameObject.Find("ButtonBlock6");

        BlockUpper = new string[7][];
        BlockLower = new string[7][];

        for (int i = 0; i < 7; i++)
        {
            BlockUpper[0] = new string[] { ":", "<", ">", "?", "|", " ", " " }; 
            BlockUpper[1] = new string[] { "Q", "W", "E", "R", "T", "Y", "U" };
            BlockUpper[2] = new string[] { "I", "O", "P", "A", "S", "D", "F" };
            BlockUpper[3] = new string[] { "G", "H", "J", "K", "L", "Z", "X" };
            BlockUpper[4] = new string[] { "C", "V", "B", "N", "M", "~", "\"" };
            BlockUpper[5] = new string[] { "!", "@", "#", "$", "%", "^", "&" };
            BlockUpper[6] = new string[] { "(", ")", "*", "_", "+", "{", "}" };

            BlockLower[0] = new string[] { ";", ",", ".", "/", "\\", " ", " " };
            BlockLower[1] = new string[] { "q", "w", "e", "r", "t", "y", "u" };
            BlockLower[2] = new string[] { "i", "o", "p", "a", "s", "d", "f" };
            BlockLower[3] = new string[] { "g", "h", "j", "k", "l", "z", "x" };
            BlockLower[4] = new string[] { "c", "v", "b", "n", "m", "`", "'" };          
            BlockLower[5] = new string[] { "1", "2", "3", "4", "5", "6", "7" };
            BlockLower[6] = new string[] { "[", "]", "8", "9", "0", "-", "=" };

            GOBlock[i].GetComponent<UILetter>().SetLetter(i,"");

            GOBlock[i].GetComponentInChildren<TextMeshProUGUI>().text = GetButtonString(BlockLower[i]);
        }
    }


    public static void SetLevel0()
    {
        level = 0;
        for (int i = 0; i < 7; i++)
        {
            GOBlock[i].GetComponentInChildren<TextMeshProUGUI>().text = 
                (state) ? GetButtonString(BlockUpper[i]) : GetButtonString(BlockLower[i]);
        }
    }

    public static void SetLevel1(int i)
    {
        child = i;
        level = 1;
        for (int j = 0; j < 7; j++)
        {
            GOBlock[j].GetComponentInChildren<TextMeshProUGUI>().text =
                (state) ? BlockUpper[i][j] : BlockLower[i][j];
        }
    }


    public static void ToggleUpperLower()
    {
        if (SceneManager.GetActiveScene().name == "Testing1")
        {
            UpdateLogger("Shift");

        }
        state = !state;
        if (level==0)
        {

            SetLevel0();
        }
        else if (level==1)
        {
            SetLevel1(child);
        }
    }

    public static void UpdateLogger(string tmp)
    {

        timer = GameObject.Find("TimeText").GetComponent<StopWatch>();
        logger = GameObject.Find("UserInputCheck").GetComponent<Text>();

        logger.text = logger.text + "\n" + "{ \"User Input\" : " + "\"" + tmp + "\" , " + "\"at\" : " + "\"" + timer.theTime + "\", " + "\"Current String\":" + "\"" + OutputText + "\" }, ";
        return;
    }

    public static void PressKey(int i)
    {
        if (level == 0)
        {
            SetLevel1(i);
        }
        else if (level==1)
        {
            string tmp = (state) ? BlockUpper[child][i] : BlockLower[child][i];
            OutputText += tmp;
                       goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
            SetLevel0();
        }     
    }

    public static void BackSpace()
    {
        if (OutputText.Length > 0)
        {
            OutputText = OutputText.Remove(OutputText.Length - 1, 1);
            goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
        }
    }

    public static void Space()
    {
        OutputText += " ";
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }

    public static void Tab()
    {
        OutputText += "\t";
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }

    public static void Enter()
    {
        OutputText += "\n";
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }

    public static void Clear()
    {
        OutputText = "";
        if (SceneManager.GetActiveScene().name == "Testing1")
        {
            UpdateLogger("ClearText");
        }
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }

}