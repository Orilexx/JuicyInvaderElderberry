using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{



    [SerializeField] float shakeSpeed = 0.3f;
    [SerializeField] float magnitude;
    private bool shakeScreen = false;
    private bool isShaking = false;
    private float timer;
    [SerializeField] float duration;

    private Vector2 originalPos;
    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if(shakeScreen == true)
        {
            if (!isShaking)
            {
                timer = duration;
            }
            Shake(duration, magnitude);
        }
    }

    public void OnPlayerCollision()
    {
        shakeScreen = true;
        isShaking = false;
    }
    

    public void Shake(float duration, float magnitude)
    {


        if(timer > 0)
        {
            isShaking = true;
            transform.localPosition = Vector2.Lerp(transform.localPosition, originalPos + Random.insideUnitCircle * magnitude * (timer / duration), shakeSpeed);

            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0f;
            transform.localPosition = originalPos;
            shakeScreen = false;
            isShaking = false;
            magnitude = 0;
        }
    }

    
}
