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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ArmyManager armyManager = gameObject.GetComponentInParent<ArmyManager>();

        if (collision.gameObject.tag == "Wall")
        {
            armyManager.movesRight = false;
            armyManager.rb.position = new Vector2(armyManager.rb.position.x, armyManager.rb.position.y - armyManager.padding);
        }
        else if (collision.gameObject.tag == "Wall2")
        {
            armyManager.movesRight = true;
            armyManager.rb.position = new Vector2(armyManager.rb.position.x, armyManager.rb.position.y - armyManager.padding);
        }
        else if (collision.gameObject.tag == "DeathCheck")
        {
            armyManager.gameManager.GetComponent<GameManager>().setLost(true);
        }
    }

}
