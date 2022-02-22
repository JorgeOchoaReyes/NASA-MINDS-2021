using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPressKey : MonoBehaviour
{
    public KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // Press key
            print("Press key");
            // ActionEvent();
        }
    }
}
