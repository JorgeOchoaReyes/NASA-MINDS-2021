using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;

public class StopTimer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    private SteamVR_LaserPointer laserPointer_left;
    private SteamVR_LaserPointer laserPointer_right;
    private SteamVR_LaserPointer laserPointer_head;
    StopWatch timer;

    protected bool selected_left;
    protected bool selected_right;
    protected bool selected_left_prev;
    protected bool selected_right_prev;
    protected bool selected_head;
    protected bool selected_head_prev;

    private float duration; 

    // Start is called before the first frame update
    void Start()
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
        timer = GameObject.Find("TimeText").GetComponent<StopWatch>();
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


    // Update is called once per frame
    void Update()
    {
        bool state_left = SteamVR_Actions.default_InteractUI[SteamVR_Input_Sources.LeftHand].stateUp;
        bool state_right = SteamVR_Actions.default_InteractUI[SteamVR_Input_Sources.RightHand].stateUp;

        if ((state_left && selected_left) || (state_right && selected_right) || (state_left && selected_head) || (state_right && selected_head))
        {
            timer.playing = false;
        }
    }
}
