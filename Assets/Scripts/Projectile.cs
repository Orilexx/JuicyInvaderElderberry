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

    private CameraController cameraController;

    private Color lerpedColor;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cameraController = gameManager.GameCamera;

    }

    // Update is called once per frame
    void Update()
    {
        if (instantiater)
            rb.MovePosition(rb.position + Vector2.up * moveSpeed * Time.deltaTime * gameManager.timeScale);
        else
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
                // INSTANTIATION
                Instantiate(particles, transform.position, transform.rotation);
                Instantiate(deathParticles, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

                // FX
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                StartCoroutine(EnemyDestruct(collision, enemy));

                // ENERGY
                if (gameManager.player._ENEMY == TYPE_ENEMY.NONE || gameManager.player._ENEMY != enemy.type)
                {
                    if(gameManager.player.energy != 4)
                    {
                        if (enemy.type != TYPE_ENEMY.CRABY)
                            gameManager.player._ENEMY = enemy.type;

                        gameManager.player.energy = 0;
                        gameManager.player.energy++;
                        gameManager.player.energyBar.sprite = gameManager.player.energyLiquids[gameManager.player.energy];
                    }
                    
                }
                else if (gameManager.player._ENEMY == enemy.type)
                {
                    if (gameManager.player.energy < 4)
                    {
                        gameManager.player.energyContainer.sprite = gameManager.player.notFullEnergy;
                        gameManager.player.energy++;
                        gameManager.player.energyBar.sprite = gameManager.player.energyLiquids[gameManager.player.energy];

                        if (gameManager.player.energy == 4)
                        {
                            gameManager.player.energyContainer.sprite = gameManager.player.fullEnergy;

                            gameManager.player.bonusUI.gameObject.SetActive(true);
                            gameManager.player.bonusUI.sprite = gameManager.bonusSprite[((int)enemy.type) + 4];
                            gameManager.player.PlaySound(gameManager.player.gameObject.GetComponent<AudioSource>(), gameManager.player.audioClips[6]);
                        }

                    }
                }

                // REMOVE FROM LIST
                collision.gameObject.GetComponentInParent<ArmyManager>().GetEnemies().Remove(collision.gameObject.GetComponent<EnemyController>());

                // SCORE
                gameManager.player.score += collision.gameObject.GetComponent<EnemyController>().score;
                gameManager.player.scoreText.text = "Score : " + gameManager.player.score;

                // DESTRUCTION
                
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
                cameraController.OnPlayerCollision();
                player.actualLife -= instantiater.damage;

                player.lifeImage.fillAmount = (float)player.actualLife / player.life;

                lerpedColor = Color.Lerp(Color.red, Color.green, (float)player.actualLife / player.life);

                player.lifeImage.color = lerpedColor;

                if (player.actualLife <= 0)
                    player.Lose();
                else
                    player.PlaySound(player.GetComponent<AudioSource>(), player.audioClips[0]);
            }
            
        }
    }

    IEnumerator EnemyDestruct(Collider2D collision, EnemyController enemy)
    {
        enemy.PlaySound(collision.gameObject.GetComponent<AudioSource>(), enemy.deathClip);
        enemy.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        enemy.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (enemy.type == TYPE_ENEMY.CRABY)
        {
            Instantiate(gameManager.immediateBonus, enemy.transform.position, gameManager.immediateBonus.transform.rotation);
        }

        yield return new WaitForSeconds(enemy.deathClip.length);

        Destroy(collision.gameObject);
    }
}
