using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
using System;

public class UIButton : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_Input_Sources handType;
    private SteamVR_LaserPointer laserPointer_left;
    private SteamVR_LaserPointer laserPointer_right;
    private SteamVR_LaserPointer laserPointer_head;

    protected bool selected_left;
    protected bool selected_right;
    protected bool selected_left_prev;
    protected bool selected_right_prev;
    protected bool selected_head;
    protected bool selected_head_prev;

    private GameObject feedback;
    private float duration;
    private float DwellTime;

    public int controlType; 

    public bool UsePointer;
    public int UseFingers;
    public static StopWatch timer;

    public void Initialize()
    {
        laserPointer_left = GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>();
        laserPointer_right = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        laserPointer_head = GameObject.Find("VRCamera").GetComponent<SteamVR_LaserPointer>();
        laserPointer_left.PointerIn += PointerInside_left;
        laserPointer_left.PointerOut += PointerOutside_left;
        laserPointer_right.PointerIn += PointerInside_right;
        laserPointer_right.PointerOut += PointerOutside_right;
        laserPointer_head.PointerIn += PointerInside_head;
        laserPointer_head.PointerOut += PointerOutside_head;
        selected_left = false;
        selected_right = false;
        selected_head = false;
        selected_left_prev = false;
        selected_right_prev = false;
        selected_head_prev = false;

        if (SceneManager.GetActiveScene().name == "Testing1")
        {
            timer = GameObject.Find("TimeText").GetComponent<StopWatch>();
        }
        controlType = 0;

        // Quad representing the feedback
        feedback= GameObject.CreatePrimitive(PrimitiveType.Quad);
        feedback.name = this.name + "feedback";
        feedback.transform.SetParent(this.transform);
        feedback.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        feedback.transform.localPosition = new Vector3(0.0f, 0.0f, -0.05f);
        feedback.transform.localRotation = Quaternion.identity;
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", GUI.SetNormalColorSelected());
        feedback.GetComponent<MeshRenderer>().material = newMaterial;

        DwellTime = 2.0f; // 2 seconds for the selection
    }

    public void PointerInside_left(object sender, PointerEventArgs e)
    {
        if (this != null)
            if (e.target.name == this.gameObject.name && selected_left == false)
            {
                selected_left = true;
            }
    }

    public void PointerOutside_left(object sender, PointerEventArgs e)
    {
        if (this != null)
            if (e.target.name == this.gameObject.name && selected_left == true)
            {
                selected_left = false;
                duration = 0.0f;
            }
    }

    public void PointerInside_right(object sender, PointerEventArgs e)
    {
        if (this != null)
            if (e.target.name == this.gameObject.name && selected_right == false)
            {
                selected_right = true;
            }
    }

    public void PointerOutside_right(object sender, PointerEventArgs e)
    {
        if (this != null)
            if (e.target.name == this.gameObject.name && selected_right == true)
            {
                selected_right = false;
                duration = 0.0f;
            }
    }

    public void PointerInside_head(object sender, PointerEventArgs e)
    {
        if (this != null)
            if (e.target.name == this.gameObject.name && selected_head == false)
            {
                selected_head = true;
            }
    }

    public void PointerOutside_head(object sender, PointerEventArgs e)
    {
        if (this != null)
            if (e.target.name == this.gameObject.name && selected_head == true)
            {
                selected_head = false;
                duration = 0.0f;
            }
    }

    public virtual void UpdateButtonColorHead()
    {
        if (this.CompareTag("UIButton") || this.CompareTag("UIValue") || this.CompareTag("UILabel"))
        {
            if (selected_head != selected_head_prev)
            {
                GameObject go = GameObject.Find(this.name + "_quad");
                if (go)
                {
                    if (selected_head)
                    {
                        Color c = GUI.SetFontColorSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                    else
                    {
                        Color c = GUI.SetFontColorUnSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                }

                selected_head_prev = selected_head;
                ColorBlock colorBlock = GetComponent<Button>().colors;
                if (selected_head)
                {
                    colorBlock.normalColor = GUI.SetNormalColorSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorSelectedButton();
                }
                else
                {
                    colorBlock.normalColor = GUI.SetNormalColorUnSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorUnSelectedButton();
                }
                GetComponent<Button>().colors = colorBlock;
            }
        }
    }

    // For the Dwell Time
    public virtual void UpdateButtonColorHeadDwellTime()
    {
        if (this.CompareTag("UIButton") || this.CompareTag("UIValue") || this.CompareTag("UILabel"))
        {
            if (selected_head != selected_head_prev)
            {
                GameObject go = GameObject.Find(this.name + "_quad");
                if (go)
                {
                    if (selected_head)
                    {
                        Color c = GUI.SetFontColorSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                    else
                    {
                        Color c = GUI.SetFontColorUnSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                }

                selected_head_prev = selected_head;
                ColorBlock colorBlock = GetComponent<Button>().colors;

                if (selected_head)
                {


                    //colorBlock.normalColor = GUI.SetNormalColorSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorSelectedButton();
                }
                else
                {
                    colorBlock.normalColor = GUI.SetNormalColorUnSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorUnSelectedButton();
                }
                GetComponent<Button>().colors = colorBlock;
            }
        }
    }

    public virtual void ToggleControls()
    {
        controlType = controlType + 1; 
        if (controlType == 4)
        {
            controlType = 0;
        }
        laserPointer_left.controlType = controlType;
        laserPointer_right.controlType = controlType;
    }

    public virtual void UpdateButtonColorHands()
    {
        if (this.CompareTag("UIButton") || this.CompareTag("UIValue") || this.CompareTag("UILabel"))
        {
            if ((selected_right != selected_right_prev) && !selected_left)
            {

                GameObject go = GameObject.Find(this.name + "_quad");
                if (go)
                {
                    if (selected_right)
                    {
                        Color c = GUI.SetFontColorSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                    else
                    {
                        Color c = GUI.SetFontColorUnSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                }

                selected_right_prev = selected_right;
                ColorBlock colorBlock = GetComponent<Button>().colors;
                if (selected_right)
                {
                    colorBlock.normalColor = GUI.SetNormalColorSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorSelectedButton();
                }
                else
                {
                    colorBlock.normalColor = GUI.SetNormalColorUnSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorUnSelectedButton();
                }
                GetComponent<Button>().colors = colorBlock;
            }
            if ((selected_left != selected_left_prev) && !selected_right)
            {
                GameObject go = GameObject.Find(this.name + "_quad");
                if (go)
                {
                    if (selected_left)
                    {
                        Color c = GUI.SetFontColorSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                    else
                    {
                        Color c = GUI.SetFontColorUnSelectedButton();
                        go.GetComponent<Renderer>().material.color = c;
                    }
                }

                selected_left_prev = selected_left;
                ColorBlock colorBlock = GetComponent<Button>().colors;
                if (selected_left)
                {
                    colorBlock.normalColor = GUI.SetNormalColorSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorSelectedButton();
                }
                else
                {
                    colorBlock.normalColor = GUI.SetNormalColorUnSelected();
                    GetComponentInChildren<TextMeshProUGUI>().color = GUI.SetFontColorUnSelectedButton();
                }
                GetComponent<Button>().colors = colorBlock;
            }
        }
        else if (this.CompareTag("UIQuad"))
        {
            if ((selected_right != selected_right_prev) && !selected_left)
            {
                selected_right_prev = selected_right;
                if (selected_right)
                    GetComponent<Renderer>().material.SetColor("_Color", GUI.SetNormalColorSelected());
                else
                    GetComponent<Renderer>().material.SetColor("_Color", GUI.SetNormalColorUnSelected());
            }
            if ((selected_left != selected_left_prev) && !selected_right)
            {
                selected_left_prev = selected_left;
                if (selected_left)
                    GetComponent<Renderer>().material.SetColor("_Color", GUI.SetNormalColorSelected());
                else
                    GetComponent<Renderer>().material.SetColor("_Color", GUI.SetNormalColorUnSelected());
            }
        }
    }

    public virtual void SetVisibility(bool visible)
    {
        if (this.CompareTag("UIButton") || this.CompareTag("UIValue") || this.CompareTag("UILabel"))
        {
            GetComponent<Button>().enabled = visible;
            float scale = (visible) ? 1.0f : 0.0f;
            GetComponent<Button>().transform.localScale = new Vector3(scale, scale, scale);

        }
        else if (this.CompareTag("UIQuad"))
        {
            GetComponent<Renderer>().enabled = visible;
            float scale = (visible) ? 1.0f : 0.0f;
            GetComponent<Button>().transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public virtual void SetText(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public virtual string GetText()
    {
        return GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public virtual void Show()
    {
        SetVisibility(true);
    }

    public virtual void Hide()
    {
        SetVisibility(false);
    }

    public void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.name == "finger_index_2_r" && controlType == 1)
        {  
            ActionEvent();
        }
    }
    
    public virtual void ActionEvent()
    {
        // Code happening 
    }


    public string Key;


    // Update is called once per frame
    public virtual void Update()
    {
        bool state_left = SteamVR_Actions.default_InteractUI[SteamVR_Input_Sources.LeftHand].stateUp;
        bool state_right = SteamVR_Actions.default_InteractUI[SteamVR_Input_Sources.RightHand].stateUp;

        if (String.Compare(Key,"")!=0)
        {
            if (Input.GetKeyDown(Key))
            {
                ActionEvent();
            }
        }

        if (controlType == 0)
        {
            UpdateButtonColorHands();
            // Control with the hands


            if ((state_left && selected_left) || (state_right && selected_right))
            {
                if (SceneManager.GetActiveScene().name == "Testing1" && timer.playing == false)
                {
                    timer.playing = true;
                }

                GameObject.FindGameObjectWithTag("Click").GetComponent<KeyClick>().pressClick(); 
                //GameObject.Find("Canvas").GetComponent<UISoundEffects>().PlaySelect();
                ActionEvent();
            }
        }

        else if (controlType == 1)
        {
            UpdateButtonColorHands();

          
            //This allows the user to still use the pointer to swtich controls or else the user must move to the UIToggleControl button to swtich controls 
            if ((state_left && selected_left) || (state_right && selected_right))
            {
                if (SceneManager.GetActiveScene().name == "Testing1" && timer.playing == false)
                {
                    timer.playing = true;
                }
                
                GameObject.FindGameObjectWithTag("Click").GetComponent<KeyClick>().pressClick();
                //GameObject.Find("Canvas").GetComponent<UISoundEffects>().PlaySelect();
                ActionEvent();
            }
        }

        else if (controlType == 2)
        {
            UpdateButtonColorHead();


            // Control with the head
            if ((state_left && selected_head) || (state_right && selected_head))
            {
                if (SceneManager.GetActiveScene().name == "Testing1" && timer.playing == false)
                {
                    timer.playing = true;
                }

                GameObject.FindGameObjectWithTag("Click").GetComponent<KeyClick>().pressClick();
                ActionEvent();
            }
        }
        
        else if (controlType==3) // Control with the Head and the DwellTime
        {
            UpdateButtonColorHeadDwellTime(); // TO DO a new version for this condition so the color changes in relation to duration (between non selection to selection)
            if (selected_head)
                duration += Time.deltaTime;




            Color startColor1 = GUI.SetNormalColorUnSelected();
            Color endColor1= GUI.SetNormalColorSelected();

            Color startColor2 = GUI.SetFontColorUnSelectedButton();
            Color endColor2 = GUI.SetFontColorSelectedButton();

            float p = duration / DwellTime;

            Color c1 = new Color();
            Color c2 = new Color();
            float dr1 = startColor1.r - endColor1.r;
            float dr2 = startColor2.r - endColor2.r;

            float dg1 = startColor1.g - endColor1.g;
            float dg2 = startColor2.g - endColor2.g;

            float db1 = startColor1.b - endColor1.b;
            float db2 = startColor2.b - endColor2.b;

            float da1 = startColor1.a - endColor1.a;
            float da2 = startColor2.a - endColor2.a;

            c1.r = startColor1.r + dr1 * p;
            c1.g= startColor1.g + dg1 * p;
            c1.b= startColor1.b + db1 * p;
            c1.a = startColor1.a + da1 * p;

            c2.r = startColor2.r + dr2 * p;
            c2.g = startColor2.g + dg2 * p;
            c2.b = startColor2.b + db2 * p;
            c2.a = startColor2.a + da2 * p;

            // y=ax+b

            SetButtonColor(c1,c2);



            if (duration >= DwellTime)
            {
                GameObject.FindGameObjectWithTag("Click").GetComponent<KeyClick>().pressClick();
                ActionEvent();
                duration = 0.0f; // Reset the duration
            }
        }
    }

    //This method modify the color of a button
    public void SetButtonColor(Color color1,Color color2)
    {
        ColorBlock colorBlock = GetComponent<Button>().colors;
        colorBlock.normalColor = color1;
        GetComponent<Button>().colors = colorBlock;
        GetComponentInChildren<TextMeshProUGUI>().color = color2;
    }
}
