using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform weapon;
    [SerializeField] Rigidbody2D prefab;

    [SerializeField] float shootCooldown;
    private float actualCooldown;

    [SerializeField] ParticleSystem particlesShoot;

    Vector2 movement;

    public int score;
    public Text scoreText;

    public int life = 50;
    [HideInInspector] public int actualLife;
    public Image lifeImage;

    public Sprite playerSprite;
    public Sprite shieldSprite;
    public bool shieldIsOn;

    public List<AudioClip> audioClips;

    public TYPE_ENEMY _ENEMY;
    [Range(0, 4)] public int energy;

    private void Start()
    {
        actualCooldown = shootCooldown;
        score = 0;
        scoreText.text = "Score : " + score;

        actualLife = life;

        lifeImage.fillAmount = actualLife / life;

        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;

        shieldIsOn = false;

        _ENEMY = TYPE_ENEMY.NONE;
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("space"))
        {
            if (actualCooldown >= shootCooldown)
            {
                particlesShoot.Play();
                Instantiate(prefab, weapon.position, prefab.transform.rotation);
                PlaySound(weapon.gameObject.GetComponent<AudioSource>());
                actualCooldown = 0;
            }
        }

        actualCooldown += Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Lose()
    {
        PlaySound(gameObject.GetComponent<AudioSource>(), audioClips[5]);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().setLost(true);
    }

    public void EnableShield()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = shieldSprite;
        shieldIsOn = true;
        PlaySound(gameObject.GetComponent<AudioSource>(), audioClips[3]);
    }

    public void DisableShield()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;
        shieldIsOn = false;
        PlaySound(gameObject.GetComponent<AudioSource>(), audioClips[1]);
    }

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
