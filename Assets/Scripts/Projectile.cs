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
                collision.gameObject.GetComponentInParent<ArmyManager>().GetEnemies().Remove(collision.gameObject.GetComponent<EnemyController>());
                Destroy(collision.gameObject);
                Destroy(gameObject);
                gameManager.player.score++;
                gameManager.player.scoreText.text = "Score : " + gameManager.player.score;
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().setLost(true);
        }
    }
}
