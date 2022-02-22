using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEvent : MonoBehaviour
{
    float angle = 0.0f;
    float degreesPerSecond = 0.1f;

    bool turn;


    private void Awake()
    {
        // Like the constructor
        // Code executed when you create the game object
    }


    // Start is called before the first frame update
    void Start()
    {
        turn = false;
    }

    public  void Toggle()
    {
        turn = !turn;
    }



    // Update is called once per frame
    void Update()
    {
        if (turn)
        {
            angle += degreesPerSecond * Time.deltaTime;
            this.transform.Rotate(0, angle, 0);
        }
    }
}
