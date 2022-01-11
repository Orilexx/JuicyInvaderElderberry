using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(AudioClip audio)
    {
        gameObject.GetComponent<AudioSource>().clip = audio;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
