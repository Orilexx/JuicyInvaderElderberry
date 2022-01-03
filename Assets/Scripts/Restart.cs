using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
    
    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("Level");
        }
    }
}
