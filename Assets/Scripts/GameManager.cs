using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D armyManager;
    [SerializeField] Rigidbody2D army;
    [SerializeField] Transform spawner;
    [SerializeField] GameObject deathScreen;
    public PlayerController player;

    private bool isLost = false;

    public List<Wave> waves;
    [SerializeField] Wave armiesManager;

    public List<Sprite> enemiesSprite;

    public List<AudioClip> specialClipsEnemy;

    public List<Sprite> bonusSprite;

    int i = 0;

    void Start()
    {
        i = 0;
        gameObject.SetActive(true);
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
            //int j = Random.Range(0, waves.Count);
            //armiesManager = Instantiate(waves[j], spawner.position, waves[j].transform.rotation);
            
            armiesManager = Instantiate(waves[i], spawner.position, waves[i].transform.rotation);
            i++;
        }
        

        //if(armyManager == null)
        //{
        //    armyManager = Instantiate(army, spawner.position, army.transform.rotation);
        //}
    }

    public void setLost(bool lost)
    {
        isLost = lost;
    }
}
