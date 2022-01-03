using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform spawnWeapon;
    public bool isInFront;

   
    void Start()
    {
        isInFront = false;
        spawnWeapon = this.transform.Find("SpawnPoint");
        Debug.Log(isInFront);
    }

    // Update is called once per frame
    void Update()
    {
        SetInFront();
    }

    private void SetInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnWeapon.transform.position, -Vector2.up * 2);

        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            isInFront = false;
        }
        else
        {
            isInFront = true;
        }
    }
    
}
