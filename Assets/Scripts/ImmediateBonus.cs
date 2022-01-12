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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UseEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }

    private void UseEffect()
    {
        if (bonusName == NAME_BONUS.LIFE)
        {
            gameManager.player.actualLife += 25;
            if (gameManager.player.actualLife > 40)
            {
                gameManager.player.actualLife = 40;
            }
            gameManager.player.lifeImage.fillAmount = (float)gameManager.player.actualLife / gameManager.player.life;
        }
        else if (bonusName == NAME_BONUS.SHIELD)
        {
            gameManager.player.EnableShield();
        }
        else if (bonusName == NAME_BONUS.TIMESTOP)
        {
            //TimeStop();
        }
        else if (bonusName == NAME_BONUS.RESERVE)
        {

        }
    }

    IEnumerator TimeStop()
    {
        float armySpeed = 0f;

        for (int i = 0; i < gameManager.armiesManager.armies.Count; i++)
        {
            if (i == 0)
            {
                armySpeed = gameManager.armiesManager.armies[i].moveSpeed;
            }
            gameManager.armiesManager.armies[i].moveSpeed = 0f;
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < gameManager.armiesManager.armies.Count; i++)
        {
            gameManager.armiesManager.armies[i].moveSpeed = armySpeed;
        }
    }
}
