using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public static class VirtualKeyboard3
{

    // List of functions for the virtual keyboard
    public static GameObject[] popupList; //List of all items in pop up
    public static GameObject popup; //Popup Object 

    public static CorrectSound ring; 
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
    public static string UserName; 


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
        goInput = GameObject.Find("ButtonInputText2");
        goOutput = GameObject.Find("ButtonOutputText2");
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
        popup = GameObject.Find("PopUp"); //Initiate Popup 

        popupList = new GameObject[7];
        
        popupList[0] = GameObject.Find("Button00");
        popupList[1] = GameObject.Find("Button11");
        popupList[2] = GameObject.Find("Button22");
        popupList[3] = GameObject.Find("Button33");
        popupList[4] = GameObject.Find("Button44");
        popupList[5] = GameObject.Find("Button55");
        popupList[6] = GameObject.Find("Button66");

        PopUpHide();


        level = 0;
        state = false;
        // create the array
        GOBlock = new GameObject[7];
        GOBlock[0] = GameObject.Find("ButtonBlock00");
        GOBlock[1] = GameObject.Find("ButtonBlock11");
        GOBlock[2] = GameObject.Find("ButtonBlock22");
        GOBlock[3] = GameObject.Find("ButtonBlock33");
        GOBlock[4] = GameObject.Find("ButtonBlock44");
        GOBlock[5] = GameObject.Find("ButtonBlock55");
        GOBlock[6] = GameObject.Find("ButtonBlock66");

        BlockUpper = new string[7][];
        BlockLower = new string[7][];

        for (int i = 0; i < 7; i++)
        {
            BlockUpper[0] = new string[] { ":", "<", ">", "?", "|", "[", "]" };
            BlockUpper[1] = new string[] { "Q", "W", "E", "R", "T", "Y", "U" };
            BlockUpper[2] = new string[] { "I", "O", "P", "A", "S", "D", "F" };
            BlockUpper[3] = new string[] { "G", "H", "J", "K", "L", "Z", "X" };
            BlockUpper[4] = new string[] { "C", "V", "B", "N", "M", "~", "\"" };
            BlockUpper[5] = new string[] { "!", "@", "#", "$", "%", "^", "&" };
            BlockUpper[6] = new string[] { "(", ")", "*", "_", "+", "{", "}" };

            BlockLower[0] = new string[] { ";", ",", ".", "/", "\\", "^", "*" };
            BlockLower[1] = new string[] { "q", "w", "e", "r", "t", "y", "u" };
            BlockLower[2] = new string[] { "i", "o", "p", "a", "s", "d", "f" };
            BlockLower[3] = new string[] { "g", "h", "j", "k", "l", "z", "x" };
            BlockLower[4] = new string[] { "c", "v", "b", "n", "m", "`", "'" };
            BlockLower[5] = new string[] { "1", "2", "3", "4", "5", "6", "7" };
            BlockLower[6] = new string[] { "[", "]", "8", "9", "0", "-", "=" };

            popupList[i].GetComponent<UILetter>().SetLetter(i, "");
            GOBlock[i].GetComponent<UILetter>().SetLetter(i, "");

            GOBlock[i].GetComponentInChildren<TextMeshProUGUI>().text = GetButtonString(BlockLower[i]);
        }
    }

    public static void SetLevel0()
    {
        PopUpHide();
        level = 0;
        for (int n = 0; n < 7; n++)
        {
            popupList[n].GetComponentInChildren<TextMeshProUGUI>().text = " "; //Clear popup 
            GOBlock[n].GetComponentInChildren<TextMeshProUGUI>().text =
                (state) ? GetButtonString(BlockUpper[n]) : GetButtonString(BlockLower[n]);
        }
        
    }

    public static void SetLevel1(int i)
    {
        child = i;
        level = 1;
        for (int j = 0; j < 7; j++)
        {
            //Set the popup wheel letters 
            popupList[j].GetComponentInChildren<TextMeshProUGUI>().text = (state) ? BlockUpper[i][j] : BlockLower[i][j];
        }
    }

    public static void ToggleShift()
    {

        //if (SceneManager.GetActiveScene().name == "Testing1")
        //{
        //    UpdateLogger("Shift2");
        //}

        state = !state;
        if (level == 0)
        {
            SetLevel0();
        }
        else if (level == 1)
        {
            SetLevel1(child);
        }
    }

    public static void PopUpPosition(int i)
    {
        popup.transform.position = GOBlock[i].transform.position + new Vector3(0,0,-.005f);

    }
    
    public static void PopUpRenderer()
    {
        popup.transform.position = new Vector3(0, 0, -.009f);
    }

    public static void UpdateLogger(string tmp)
    {

        timer = GameObject.Find("TimeText").GetComponent<StopWatch>();
        logger = GameObject.Find("UserInputCheck").GetComponent<Text>();

        //this if removes the first comma in the first log
        logger.text = logger.text + "\n" + "{ \"User Input\" : " + "\"" + tmp + "\" , " + "\"at\" : " + "\"" + timer.theTime + "\", " + "\"Current String\":" + "\"" + OutputText + "\" }, ";
        return;
    }
    public static void PopUpHide()
    {
        popup.transform.position = new Vector3(0, 0, -.009f);
    }

    public static void PressKey(int i)
    {
        if (level == 0)
        {
            PopUpRenderer();
            PopUpPosition(i); //Position Popup ontop of the button clicked
            SetLevel1(i);
        }
        else if (level == 1)
        {
            string tmp = (state) ? BlockUpper[child][i] : BlockLower[child][i];
            OutputText += tmp;
            goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";     
            SetLevel0();
            PopUpHide();
        }
        else
        {
            PopUpHide();
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
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }

}