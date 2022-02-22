using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClick : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip keyClick;
    private AudioSource clickSource;

    void Start()
    {
        clickSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressClick()
    {
        clickSource.PlayOneShot(keyClick);
    }
}
