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


    [SerializeField] List<EnemyController> frontEnemies;

 
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



    private void SetRowEnemy()
    {
        frontEnemies.Clear();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].isInFront)
            {
                    frontEnemies.Add(enemies[i]);
            }
        }
    }
}
