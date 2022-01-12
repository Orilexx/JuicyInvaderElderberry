using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NAME_BONUS
{
    LIFE,
    SHIELD,
    TIMESTOP,
    RESERVE,
    //LASER,
    //BOMB,
    //PIERCE,
    //RICOCHET,
    //MULTIPLE,
    //ENERGY
}

public class ImmediateBonus : MonoBehaviour
{
    public NAME_BONUS bonusName;

    public GameManager gameManager;

    public float speed = -50;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        bonusName = (NAME_BONUS)Random.Range(0, 3);

        gameObject.GetComponent<SpriteRenderer>().sprite = gameManager.bonusSprite[((int)bonusName)];


    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.MovePosition(rb.position + Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
