using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingActor : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 360.0f) * 2 * Time.deltaTime * gameManager.timeScale);
    }
}
