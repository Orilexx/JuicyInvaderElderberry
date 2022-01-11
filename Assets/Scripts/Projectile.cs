using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject particles;
    [SerializeField] GameObject deathParticles;

    private GameManager gameManager;

    /*[HideInInspector]*/ public EnemyController instantiater;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + Vector2.up * moveSpeed * Time.deltaTime);
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag != "EnemyProj")
            {
                Instantiate(particles, transform.position, transform.rotation);
                Instantiate(deathParticles, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                enemy.PlaySound(collision.gameObject.GetComponent<AudioSource>(), enemy.deathClip);

                collision.gameObject.GetComponentInParent<ArmyManager>().GetEnemies().Remove(collision.gameObject.GetComponent<EnemyController>());

                gameManager.player.score += collision.gameObject.GetComponent<EnemyController>().score;
                gameManager.player.scoreText.text = "Score : " + gameManager.player.score;

                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player.shieldIsOn)
            {
                player.DisableShield();
            }
            else
            {
                player.actualLife -= instantiater.damage;

                player.lifeImage.fillAmount = (float)player.actualLife / player.life;

                if (player.actualLife <= 0)
                    player.Lose();
                else
                    player.PlaySound(player.GetComponent<AudioSource>(), player.audioClips[0]);
            }
            
        }
    }
}
