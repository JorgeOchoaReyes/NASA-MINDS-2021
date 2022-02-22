using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public static class VirtualKeyboard0
{

    // List of functions for the virtual keyboard
    public static GameObject goInput;
    public static GameObject goOutput;
    public static GameObject[] gokeys; // list of 
    public static string[] UpperCase = { "~","!","@","#","$","%","^","&","*","(",")","_","+",   // 13 keys
                                         "Q","W", "E","R","T","Y","U","I","O","P","{","}","|",  // 13 keys
                                         "A","S","D","F","G","H","J","K","L",":","\"", // 11 keys
                                         "Z","X","C","V","B","N","M","<",">","?"}; // 10 keys

    public static string[] LowerCase = {"`","1", "2", "3", "4", "5", "6", "7", "8","9","0","-","=", // 13 keys
                                          "q","w","e","r","t","y","u","i","o","p","[","]","\\", // 13 keys
                                          "a","s","d","f","g","h","j","k","l",";","'", // 11 keys
                                          "z","x","c","v","b","n","m",",",".","/" }; // 10 keys

    public static bool state;

    public static string OutputText;
    public static string InputText;

    public static CorrectSound ring;
    public static EventDataList EDL;
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
        goInput = GameObject.Find("ButtonInputText0");
        goOutput = GameObject.Find("ButtonOutputText0");
        goInput.GetComponentInChildren<TextMeshProUGUI>().text = InputText;
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";

        EDL = new EventDataList();
    }


    public static void SetKeys()
    {
        int nkeys = UpperCase.Length;
        Debug.Log("Nkeys: " + nkeys + "\n");
        gokeys = new GameObject[nkeys];
        state = false;
        for (int i=0;i<nkeys;i++)
        {
            gokeys[i] = GameObject.Find("ButtonKey" + i);
            gokeys[i].GetComponent<UILetter>().SetLetter(i, LowerCase[i]);
        }
    }

    public static void ToggleUpperLower()
    {

        if (SceneManager.GetActiveScene().name == "Testing1")
        {
            UpdateLogger("Shift");

        }

        state = !state;
        int nkeys = UpperCase.Length;
        if (state)
        {
            for (int i = 0; i < nkeys; i++)
                gokeys[i].GetComponent<UILetter>().SetLetter(i, UpperCase[i]);
        }
        else
        {
            for (int i = 0; i < nkeys; i++)
                gokeys[i].GetComponent<UILetter>().SetLetter(i, LowerCase[i]);
        }
    }

    public static void UpdateLogger(string tmp)
    {

            timer = GameObject.Find("TimeText").GetComponent<StopWatch>();
            logger = GameObject.Find("UserInputCheck").GetComponent<Text>();

            //this if removes the first comma in the first log
            logger.text = logger.text + "\n" + "{ \"User Input\" : " + "\"" + tmp + "\" , " + "\"at\" : " + "\"" + timer.theTime + "\", " + "\"Current String\":" + "\"" + OutputText  + "\" }, ";
            return;
    }

    public static void PressKey(int i)
    {
        string tmp = (state) ? UpperCase[i] : LowerCase[i];
        OutputText += tmp;
        goOutput.GetComponentInChildren<TextMeshProUGUI>().text = OutputText + "_";
    }


    public static void BackSpace()
    {
        if (OutputText.Length > 0)
        {
            OutputText = OutputText.Remove(OutputText.Length - 1, 1);

            if (SceneManager.GetActiveScene().name == "Testing")
            {
                UpdateLogger("BackSpace");
                
            }

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
        if (SceneManager.GetActiveScene().name == "Testing1")
        {
            UpdateLogger("Tab");
        }
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