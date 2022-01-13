using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class JuicyManager : MonoBehaviour
{
    public static JuicyManager instance;

    [Header("Input 1")]
    public List<Parallax> parallaxes;
    public LookAt eye;
    public bool liveBackground;

    [Header("Input 2")]
    public bool fxPlayerShot;

    [Header("Input 3")]
    public bool fxEnemyDeath;

    [Header("Input 4")]
    public CameraController jmCamera;

    [Header("Input 5")]
    public Volume volume;
    private ColorAdjustments saturation;
    public bool timeFreeze;

    [Header("Input 6")]
    public bool fxEnemyShot;

    [Header("Input 7")]
    private Vignette vignette;
    public List<GameObject> lights2D;
    private bool postProcess;

    [Header("Input 8")]
    public AudioMixer audioMixer;
    private bool volumeAudioMixer;

    [Header("Input 9")]
    public PlayerController player;
    public bool fxUI;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ColorAdjustments>(out saturation);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            liveBackground = !liveBackground;

            eye.enabled = liveBackground;

            for(int i = 0; i < parallaxes.Count; i++)
            {
                parallaxes[i].enabled = liveBackground;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fxPlayerShot = !fxPlayerShot;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fxEnemyDeath = !fxEnemyDeath;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            jmCamera.enabled = !jmCamera.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            timeFreeze = !timeFreeze;

            saturation.active = timeFreeze;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            fxEnemyShot = !fxEnemyShot;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            postProcess = !postProcess;

            vignette.active = postProcess;

            for (int i = 0; i < lights2D.Count; i++)
            {
                lights2D[i].SetActive(postProcess);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            volumeAudioMixer = !volumeAudioMixer;

            if (volumeAudioMixer)
                audioMixer.SetFloat("volume", -80);
            else
                audioMixer.SetFloat("volume", 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            fxUI = !fxUI;

            if (!fxUI)
            {
                player.gameObject.GetComponent<SpriteRenderer>().sprite = player.playerSprite;
                player.lifeContainer.sprite = player.notProtectedLife;
                player.lifeImage.color = Color.green;
            }
            else
            {
                if (player.shieldIsOn)
                {
                    player.gameObject.GetComponent<SpriteRenderer>().sprite = player.shieldSprite;
                    player.lifeContainer.sprite = player.protectedLife;
                }
                player.lifeImage.color = Color.Lerp(Color.red, Color.green, player.lifeImage.fillAmount);
            }
        }


    }
}
