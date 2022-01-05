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

        Physics2D.queriesHitTriggers = false;
    }
    
    public bool IsInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnWeapon.transform.position, -Vector2.up * 2);

        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
}
