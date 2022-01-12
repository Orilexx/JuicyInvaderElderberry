using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TYPE_ENEMY
{
    MAI,
    ECHO,
    SANTEN,
    NARUTO,
    MOUCHE,
    BOOMER,
    CRABY,
    NONE
}

public class EnemyController : MonoBehaviour
{
    
    public Transform spawnWeapon;
    public bool isInFront;
    [HideInInspector] public int score;
    [HideInInspector] public int damage;

    public TYPE_ENEMY type;

    GameManager gameManager;

    public AudioClip deathClip;
    public AudioClip specialEnemyClip;

    void Start()
    {
        isInFront = false;
        spawnWeapon = this.transform.Find("SpawnPoint");

        Physics2D.queriesHitTriggers = false;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (type != TYPE_ENEMY.CRABY)
            deathClip = gameManager.armiesManager.deathClip;
        else
            deathClip = gameManager.crabyWave.deathClip;

        if (type == TYPE_ENEMY.MAI)
        {
            score = 150;
            damage = 10;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[0];
        }
        else if (type == TYPE_ENEMY.ECHO)
        {
            score = 100;
            damage = 10;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[1];
        }
        else if (type == TYPE_ENEMY.SANTEN)
        {
            score = 200;
            damage = 10;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[2];
            specialEnemyClip = gameManager.armiesManager.santenShieldClip;
        }
        else if (type == TYPE_ENEMY.NARUTO)
        {
            score = 200;
            damage = 20;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[3];
        }
        else if (type == TYPE_ENEMY.MOUCHE)
        {
            score = 50;
            damage = 5;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[4];
        }
        else if (type == TYPE_ENEMY.BOOMER)
        {
            score = 100;
            damage = 30;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[5];
        }
        else if (type == TYPE_ENEMY.CRABY)
        {
            score = 1000;
            damage = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.enemiesSprite[6];
        }
    }
    
    public bool IsInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnWeapon.transform.position, -Vector2.up * 2);

        if (type != TYPE_ENEMY.CRABY)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
                return false;
            else
                return true;
        }
        else
            return true;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ArmyManager armyManager = gameObject.GetComponentInParent<ArmyManager>();

        if (collision.gameObject.tag == "Wall")
        {
            if (type != TYPE_ENEMY.CRABY)
            {
                armyManager.movesRight = false;
                armyManager.rb.position = new Vector2(armyManager.rb.position.x, armyManager.rb.position.y - armyManager.padding);
            }
        }
        else if (collision.gameObject.tag == "Wall2")
        {
            if (type != TYPE_ENEMY.CRABY)
            {
                armyManager.movesRight = true;
                armyManager.rb.position = new Vector2(armyManager.rb.position.x, armyManager.rb.position.y - armyManager.padding);
            }
        }
        else if (collision.gameObject.tag == "DeathCheck")
        {
            armyManager.gameManager.player.actualLife -= damage;

            armyManager.gameManager.player.lifeImage.fillAmount = (float)armyManager.gameManager.player.actualLife / armyManager.gameManager.player.life;

            if (armyManager.gameManager.player.actualLife <= 0)
                armyManager.gameManager.player.Lose();
            else
                armyManager.gameManager.player.PlaySound(armyManager.gameManager.player.GetComponent<AudioSource>(), armyManager.gameManager.player.audioClips[0]);

            //armyManager.gameManager.GetComponent<GameManager>().setLost(true);
        }
        else if (collision.gameObject.tag == "CrabyDestroyer")
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
