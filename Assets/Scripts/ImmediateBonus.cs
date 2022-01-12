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

        bonusName = (NAME_BONUS)Random.Range(0, 4);

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BonusDestroyer")
        {
            Destroy(gameObject);
        }
    }

    private void UseEffect()
    {
        if (bonusName == NAME_BONUS.LIFE)
        {
            gameManager.player.actualLife += 25;
            Debug.Log(gameManager.player.actualLife += 25);
            if (gameManager.player.actualLife > 40)
            {
                gameManager.player.actualLife = 40;
            }
            gameManager.player.lifeImage.fillAmount = (float)gameManager.player.actualLife / gameManager.player.life;

            Destroy(gameObject);
        }
        else if (bonusName == NAME_BONUS.SHIELD)
        {
            gameManager.player.EnableShield();

            Destroy(gameObject);
        }
        else if (bonusName == NAME_BONUS.TIMESTOP)
        {
            StartCoroutine(TimeStop());
        }
        else if (bonusName == NAME_BONUS.RESERVE)
        {
            Debug.Log("RAS");

            Destroy(gameObject);
        }
    }

    IEnumerator TimeStop()
    {
        gameManager.timeScale = 0;
        Debug.Log("Timestop");

        yield return new WaitForSeconds(4f);

        gameManager.timeScale = 1;
        Debug.Log("Timeplay");

        Destroy(gameObject);
    }
}
