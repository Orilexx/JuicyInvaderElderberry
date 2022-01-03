using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    [SerializeField] bool movesRight;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] float padding;

    private GameManager gameManager;

    [SerializeField] Rigidbody2D prefab;

    [SerializeField] float timer = 3f;
    private float timeLeft;

    public float dist = 0f;

    [SerializeField] List<EnemyController> frontEnemies;

    private void Awake()
    {
        //SetRowEnemy();
    }
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        timeLeft = timer;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemies.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (movesRight)
            {

                rb.MovePosition(rb.position + Vector2.right * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + Vector2.left * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            timeLeft = timer;

            int i = Random.Range(0, frontEnemies.Count);
            Instantiate(prefab, frontEnemies[i].spawnWeapon.position, prefab.transform.rotation);

        }


        SetRowEnemy();

    }


    public List<EnemyController> GetEnemies()
    {
        return enemies;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (movesRight)
            {
                movesRight = false;
            }
            else
            {
                movesRight = true;
            }
            rb.position = new Vector2(rb.position.x, rb.position.y - padding);

        }
        else if (collision.gameObject.tag == "DeathCheck")
        {
            gameManager.GetComponent<GameManager>().setLost(true);
        }
    }

    private void SetRowEnemies()
    {
        float distMin = Mathf.Infinity;

        // Calcul de la distance minimum
        for (int i = 0; i < enemies.Count; i++)
        {
            float playerPosition = gameManager.player.transform.position.y;
            dist = enemies[i].transform.position.y - playerPosition;

            if (dist < distMin)
            {
                distMin = dist;
            }

        }

        // Add To Front Row
        for (int y = 0; y < enemies.Count; y++)
        {
            float distEnemy = enemies[y].transform.position.y - gameManager.player.transform.position.y;

            if (distEnemy == dist)
            {
                frontEnemies.Add(enemies[y]);
            }
        }
    }

    private void SetRowEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].isInFront)
            {
                if(!frontEnemies.Contains(enemies[i]))
                    frontEnemies.Add(enemies[i]);
            }
        }
    }
}
