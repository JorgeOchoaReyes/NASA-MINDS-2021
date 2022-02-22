using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeLogs : MonoBehaviour
{
    public string alpha;
    public string delta;
    public float time;
    public string texttime;

    public GameObject[] all; 

    public GameObject textInput;
    public GameObject textOutput;
    public int keyboardType;
    public int keyboardTypeDelta; 
    static CorrectSound ring; 
    public static StopWatch timer;
    bool trigger; 

    public static Text logger;

    // Start is called before the first frame update
    void Start()
    {
        ring = GameObject.FindGameObjectWithTag("Sound").GetComponent<CorrectSound>();
        logger = GameObject.Find("UserInputCheck").GetComponent<Text>();
        timer = GameObject.Find("TimeText").GetComponent<StopWatch>();
        time = timer.theTime;
        trigger = false;
        keyboardType = GameObject.Find("ToggleKeyboard").GetComponent<ToggleKeyboard>().current;

    }

    void setKeyboard(int b)
    {

        if(b == 0)
        {
            textInput = GameObject.Find("ButtonInputText0");
            textOutput = GameObject.Find("ButtonOutputText0");
            alpha = textOutput.GetComponentInChildren<TextMeshProUGUI>().text;
            trigger = false;
        }
        else if (b == 1)
        {
            textInput = GameObject.Find("ButtonInputText1");
            textOutput = GameObject.Find("ButtonOutputText1");
            alpha = textOutput.GetComponentInChildren<TextMeshProUGUI>().text;
            trigger = false;
        }
        else if (b == 2)
        {
            textInput = GameObject.Find("ButtonInputText2");
            textOutput = GameObject.Find("ButtonOutputText2");
            alpha = textOutput.GetComponentInChildren<TextMeshProUGUI>().text;
            trigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!textInput || !textOutput)
        {
            textInput = GameObject.Find("ButtonInputText0");
            textOutput = GameObject.Find("ButtonOutputText0");
            alpha = textOutput.GetComponentInChildren<TextMeshProUGUI>().text;
        }

        keyboardTypeDelta = GameObject.Find("ToggleKeyboard").GetComponent<ToggleKeyboard>().current;
        //Initiate our input/output
        if (keyboardType != keyboardTypeDelta)
        {
            setKeyboard(keyboardTypeDelta);
            keyboardType = keyboardTypeDelta;
        }

        string output = textOutput.GetComponentInChildren<TextMeshProUGUI>().text;
        output = output.Remove(output.Length - 1);

        //Is output = input?
        if (textInput.GetComponentInChildren<TextMeshProUGUI>().text == output && !trigger)
        {
            trigger = true; 
            ring.PlayKeyClick();
            //Logs Time and Message of Done
            logger.text = logger.text + "\n" + "{ \"User Input\" : " + "\"" + output[output.Length - 1] + "\" , " + "\"at\" : " + "\"" + timer.theTime + "\", " + "\"Current String\":" + "\"" + output + "\" }, " ;
            timer.clickPause();

            string str = " [ \n " + logger.text + " ] ";
            int id = Random.Range(-1000, 1000);
            string txtId = id.ToString();
            System.IO.File.WriteAllText(Application.streamingAssetsPath + "/" + "Testing" + "_" + txtId + "_type" + keyboardTypeDelta + ".json", str);

        }

        //Output listener 
        delta = textOutput.GetComponentInChildren<TextMeshProUGUI>().text;

        //Has the user inputted something?
        if(delta != alpha)
        {
            if (delta.Length < alpha.Length)
            {
                alpha = delta;
                logger.text =  logger.text + "\n" + "{ \"User Input\" : " + "\"" + "BACKSPACE" + "\" , " + "\"at\" : " + "\"" + timer.theTime + "\", " + "\"Current String\":" + "\"" + output + "\" }," ; 
            }
            else
            {
                alpha = delta;
                
                logger.text = logger.text + "\n" + "{ \"User Input\" : " + "\"" + delta[delta.Length - 2] + "\" , " + "\"at\" : " + "\"" + timer.theTime + "\", " + "\"Current String\":" + "\"" + output + "\" }," ;
            }

}
    }
}
