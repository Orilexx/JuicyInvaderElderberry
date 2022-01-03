using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{

    private bool lightText = false;
    private Color color;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lightText) { 
            if(GetComponent<Text>().color.a < 1)
            {
                color = GetComponent<Text>().color;
                color.a += Time.fixedDeltaTime;
                GetComponent<Text>().color = color;
            }
            else
            {
                lightText = false;
            }
            
        } else
        {
            if (GetComponent<Text>().color.a > 0)
            {
                color = GetComponent<Text>().color;
                color.a -= Time.fixedDeltaTime;
                GetComponent<Text>().color = color;
            }
            else
            {
                lightText = true;
            }
        }
    }
}
