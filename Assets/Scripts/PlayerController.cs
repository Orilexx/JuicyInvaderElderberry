using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TYPE_PROJECTILE
{
    BASIC,
    PIERCE,
    MULTIPLE,
    ENERGY
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform weapon;
    [SerializeField] GameObject prefab;

    public TYPE_PROJECTILE projectileType;

    [SerializeField] float shootCooldown;
    private float actualCooldown;

    [SerializeField] ParticleSystem particlesShoot;

    Vector2 movement;
    GameManager gameManager;

    public int score;
    public Text scoreText;

    [Header("Life")]
    public int life = 50;
    [HideInInspector] public int actualLife;
    public Image lifeImage;


    [Header("Shield")]
    public Sprite playerSprite;
    public Sprite shieldSprite;
    public bool shieldIsOn;
    [Space(10)]

    public Image lifeContainer;
    public Sprite protectedLife;
    public Sprite notProtectedLife;
    [Space(10)]

    public List<AudioClip> audioClips;

    [Header("Projectiles")]
    public List<AudioClip> shootClips;
    public List<GameObject> prefabs;

    [HideInInspector] public TYPE_ENEMY _ENEMY;

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
    [HideInInspector] public Color lerpedColor = Color.green;


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

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (actualCooldown >= shootCooldown)
            {
                if (JuicyManager.instance.fxPlayerShot)
                    particlesShoot.Play();

                PlaySound(weapon.gameObject.GetComponent<AudioSource>(), shootClips[((int)projectileType)]);
                prefab = prefabs[((int)projectileType)];

                Instantiate(prefab, weapon.position, prefab.transform.rotation);
                

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
        lifeContainer.sprite = protectedLife;
        shieldIsOn = true;
        PlaySound(gameObject.GetComponent<AudioSource>(), audioClips[3]);
    }

    public void DisableShield()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;
        lifeContainer.sprite = notProtectedLife;
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
        energyContainer.sprite = notFullEnergy;

        if (bonusUI.sprite == gameManager.bonusSprite[4] || bonusUI.sprite == gameManager.bonusSprite[7])
        {
            projectileType = TYPE_PROJECTILE.PIERCE;
        }
        else if (bonusUI.sprite == gameManager.bonusSprite[5] || bonusUI.sprite == gameManager.bonusSprite[8])
        {
            projectileType = TYPE_PROJECTILE.MULTIPLE;
        }
        else if (bonusUI.sprite == gameManager.bonusSprite[6] || bonusUI.sprite == gameManager.bonusSprite[9])
        {
            projectileType = TYPE_PROJECTILE.ENERGY;
        }

    }
}
