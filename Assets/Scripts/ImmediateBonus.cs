using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    public Volume volume;

    private ColorAdjustments colorAdj;
    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        bonusName = (NAME_BONUS)Random.Range(0, 4);

        ReloadBonusLife();
        ReloadBonusShield();

        volume = GameObject.FindGameObjectWithTag("Volume").GetComponent<Volume>();
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
            gameManager.player.PlaySound(gameManager.player.gameObject.GetComponent<AudioSource>(), gameManager.player.audioClips[2]);

            if (gameManager.player.actualLife > gameManager.player.life)
            {
                gameManager.player.actualLife = gameManager.player.life;
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
            gameManager.player.energy = 4;
            gameManager.player.energyBar.sprite = gameManager.player.energyLiquids[gameManager.player.energy];
            gameManager.player.energyContainer.sprite = gameManager.player.fullEnergy;

            gameManager.player.bonusUI.gameObject.SetActive(true);
            gameManager.player.bonusUI.sprite = gameManager.bonusSprite[Random.Range(4,10)];

            Destroy(gameObject);
        }
    }

    IEnumerator TimeStop()
    {

        if (volume.profile.TryGet<ColorAdjustments>(out colorAdj))
        {
            colorAdj.saturation.value = -63f;
        }
        gameManager.timeScale = 0;
        
        gameObject.GetComponent<SpriteRenderer>().sprite = null;

        gameManager.player.PlaySound(gameManager.player.gameObject.GetComponent<AudioSource>(), gameManager.player.audioClips[4]);

        yield return new WaitForSeconds(4f);

        gameManager.timeScale = 1;
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdj))
        {
            colorAdj.saturation.value = 0f;
        }

        Destroy(gameObject);
    }

    private void ReloadBonusLife()
    {
        if (gameManager.player.actualLife == gameManager.player.life && bonusName == NAME_BONUS.LIFE)
        {
            bonusName = (NAME_BONUS)Random.Range(0, 4);
            ReloadBonusLife();
        }
    }

    private void ReloadBonusShield()
    {
        if (gameManager.player.shieldIsOn && bonusName == NAME_BONUS.SHIELD)
        {
            bonusName = (NAME_BONUS)Random.Range(0, 4);
            ReloadBonusShield();
        }
    }
}
