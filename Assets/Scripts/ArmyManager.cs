using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    [SerializeField] bool movesRight;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] List<Transform> enemies;
    [SerializeField] float padding;

    private GameObject gameManager;



    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
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


    public List<Transform> GetEnemies()
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
}
