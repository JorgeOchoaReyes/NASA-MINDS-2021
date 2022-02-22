using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using TMPro;
using UnityEngine.UI;

public class ButtonOK : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_Input_Sources handType;
    private SteamVR_LaserPointer laserPointer_left;
    private SteamVR_LaserPointer laserPointer_right;
    protected bool selected_left;
    protected bool selected_right;
    protected bool selected_left_prev;
    protected bool selected_right_prev;


    // Start is called before the first frame update
    void Start()
    {
        laserPointer_left = GameObject.Find("LeftHand").GetComponent<SteamVR_LaserPointer>();
        laserPointer_right = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        laserPointer_left.PointerIn += PointerInside_left;
        laserPointer_left.PointerOut += PointerOutside_left;
        laserPointer_right.PointerIn += PointerInside_right;
        laserPointer_right.PointerOut += PointerOutside_right;
        selected_left = false;
        selected_right = false;
        selected_left_prev = false;
        selected_right_prev = false;

        UpdateButtonColor();
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
            }
    }

    public virtual void UpdateButtonColor()
    {
        if ((selected_right != selected_right_prev))
        {
            selected_right_prev = selected_right;
            ColorBlock colorBlock = GetComponent<Button>().colors;
            if (selected_right)
            {
                colorBlock.normalColor = Color.red;
                GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            }
            else
            {
                colorBlock.normalColor = Color.blue;
                GetComponentInChildren<TextMeshProUGUI>().color = Color.magenta;
            }
            GetComponent<Button>().colors = colorBlock;

        }
        if ((selected_left != selected_left_prev))
        {
            selected_left_prev = selected_left;
            ColorBlock colorBlock = GetComponent<Button>().colors;
            if (selected_left)
            {
                colorBlock.normalColor = Color.red;
                GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            }
            else
            {
                colorBlock.normalColor = Color.blue;
                GetComponentInChildren<TextMeshProUGUI>().color = Color.magenta;
            }
            GetComponent<Button>().colors = colorBlock;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool state_left = SteamVR_Actions.default_InteractUI[SteamVR_Input_Sources.LeftHand].stateUp;
        bool state_right = SteamVR_Actions.default_InteractUI[SteamVR_Input_Sources.RightHand].stateUp;

        UpdateButtonColor();
        if ((state_left && selected_left) || (state_right && selected_right))
        {
            GameObject.Find("Cube1").GetComponent<CubeEvent>().Toggle();
        }
    }
}
