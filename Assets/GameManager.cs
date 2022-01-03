using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D armyManager;
    [SerializeField] Rigidbody2D army;
    [SerializeField] Transform spawner;
    [SerializeField] GameObject deathScreen;

    private bool isLost = false;

    // Update is called once per frame
    void Update()
    {
        if(isLost)
        {
            armyManager = null;
            deathScreen.SetActive(true);
            gameObject.SetActive(false);
        }


        if(armyManager == null)
        {
            armyManager = Instantiate(army, spawner.position, army.transform.rotation);
        }
    }

    public void setLost(bool lost)
    {
        isLost = lost;
    }
}
