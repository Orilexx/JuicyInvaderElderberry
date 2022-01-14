using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D armyManager;
    [SerializeField] Rigidbody2D army;
    [SerializeField] Transform spawner;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject winScreen;
    public PlayerController player;

    private bool isLost = false;
    public CameraController GameCamera;

    public List<Wave> waves;
    public Wave armiesManager;

    public Wave crabyWavePrefab;
    public Wave crabyWave;

    public List<Sprite> enemiesSprite;
    public Sprite santen;

    public List<Sprite> bonusSprite;

    public int timeScale = 1;

    public ImmediateBonus immediateBonus;

    int i = 0;

    void Start()
    {
        i = 0;
        gameObject.SetActive(true);
        winScreen.SetActive(false);
        deathScreen.SetActive(false);
        armiesManager = null;
        Time.timeScale = 1;



    }

    // Update is called once per frame
    void Update()
    {
        if(isLost)
        {
            //armyManager = null;
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }

        
        if (armiesManager == null && !isLost)
        {
            if (i == waves.Count)
                winScreen.SetActive(true);
            else
            {
                armiesManager = Instantiate(waves[i], spawner.position, waves[i].transform.rotation);
                i++;
            }
            
        }
    }

    public void setLost(bool lost)
    {
        isLost = lost;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void PlaySound()
    {
        EventSystem.current.gameObject.GetComponent<AudioSource>().Play();
    }
}
