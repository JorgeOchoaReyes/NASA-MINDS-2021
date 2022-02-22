using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip keyClick;
    private AudioSource clickSource;
    void Start()
    {
        clickSource = gameObject.AddComponent<AudioSource>();

    }
    public void PlayKeyClick()
    {
        clickSource.PlayOneShot(keyClick);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
