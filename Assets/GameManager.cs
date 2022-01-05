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

    [SerializeField] List<Wave> waves;
    [SerializeField] Wave armiesManager;

    // Update is called once per frame
    void Update()
    {
        if(isLost)
        {
            armyManager = null;
            deathScreen.SetActive(true);
            gameObject.SetActive(false);
        }

        if (armiesManager == null)
        {
            int j = Random.Range(0, waves.Count);
            armiesManager = Instantiate(waves[j], spawner.position, waves[j].transform.rotation);
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
