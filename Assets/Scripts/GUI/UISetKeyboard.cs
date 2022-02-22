using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// "Waltz, bad nymph, for quick jigs vex."(28 letters)
// "How vexingly quick daft zebras jump!"(30 letters)


public class UISetKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VirtualKeyboard0.SetInputOutputText("Hello");
        //VirtualKeyboard0.SetInputOutputText("How vexingly quick daft zebras jump!");
        VirtualKeyboard0.SetKeys();


        VirtualKeyboard1.SetInputOutputText("Hello");
        VirtualKeyboard1.SetKeys();

        //VirtualKeyboard3.SetInputOutputText("Waltz, bad nymph, for quick jigs vex.");
        VirtualKeyboard3.SetInputOutputText("Hello");
        VirtualKeyboard3.SetKeys();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
