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
    public int actualLife;
    public Image lifeImage;

    public Sprite playerSprite;
    public Sprite shieldSprite;
    public bool shieldIsOn;

    public List<AudioClip> audioClips;

    public TYPE_ENEMY _ENEMY;

    public Image bonusUI;

    [Header("Energy")]

    [Range(0, 4)] public int energy;
    [Space(10)]

    public SpriteRenderer energyContainer;
    public SpriteRenderer energyBar;
    [Space(10)]

    public Sprite fullEnergy;
    public Sprite notFullEnergy;
    [Space(10)]

    public List<Sprite> energyLiquids;
    Color lerpedColor = Color.green;

    private void Start()
    {
        actualCooldown = shootCooldown;
        score = 0;
        scoreText.text = "Score : " + score;

        actualLife = life;

        lifeImage.fillAmount = actualLife / life;

        lerpedColor = Color.Lerp(Color.red, Color.green, actualLife / life);

        lifeImage.color = lerpedColor;

        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;

        shieldIsOn = false;

        _ENEMY = TYPE_ENEMY.NONE;

        energy = 0;
        energyBar.sprite = energyLiquids[energy];

        bonusUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
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

        if (Input.GetKeyDown(KeyCode.Mouse1) && energy == 4)
        {
            UseBonus();
        }
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

    public void UseBonus()
    {
        energy = 0;
        energyBar.sprite = energyLiquids[energy];
        bonusUI.gameObject.SetActive(false);

    }
}
