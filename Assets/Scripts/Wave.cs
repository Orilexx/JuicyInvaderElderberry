using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public List<ArmyManager> armies;

    [SerializeField] Rigidbody2D prefab;
    [SerializeField] GameObject boomerShootPrefab;

    [SerializeField] float timer = 3f;
    private float timeLeft;

    [SerializeField] List<EnemyController> frontEnemies;

    GameManager gameManager;

    public AudioClip deathClip;
    public AudioClip santenShieldClip;

    public float crabySpawnTime = 5f;
    private float crabyTimeLeft;
    private bool crabyInstance;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        timeLeft = timer;

        crabyTimeLeft = crabySpawnTime;
        crabyInstance = false;

    }

    void Update()
    {

        timeLeft -= Time.deltaTime * gameManager.timeScale;

        //if (gameObject != gameManager.crabyWave)
        //{

        //}
        
        if (timeLeft <= 0)
        {
            timeLeft = timer;
            
            SetRowEnemy();

            InstanceProj();

        }


        crabyTimeLeft -= Time.deltaTime * gameManager.timeScale;

        if (crabyTimeLeft <= 0 && gameManager.crabyWave == null && !crabyInstance)
        {
            // CRABY INSTANTIATION

            int k = Random.Range(0, 2);
            float positionY = Random.Range(-1.5f, 3f);

            if (k == 0)
            {
                Vector3 position1 = new Vector3(-9.7f, positionY, 0);

                Wave crabyW = Instantiate(gameManager.crabyWavePrefab, position1, gameManager.crabyWavePrefab.transform.rotation);
                gameManager.crabyWave = crabyW;
                crabyW.armies[0].movesRight = true;
            }
            else if (k == 1)
            {
                Vector3 position2 = new Vector3(9.7f, positionY, 0);

                Wave crabyW = Instantiate(gameManager.crabyWavePrefab, position2, gameManager.crabyWavePrefab.transform.rotation);
                gameManager.crabyWave = crabyW;
                crabyW.armies[0].movesRight = false;
            }

            crabyInstance = true;
        }

    }

    void FixedUpdate()
    {
        if(armies.Count == 1)
        {
            if (armies[0].GetEnemies().Count == 0)
            {
                Destroy(gameObject);
            }
        }
        else if(armies.Count == 2)
        {
            if (armies[0].GetEnemies().Count == 0 && armies[1].GetEnemies().Count == 0)
            {
                Destroy(gameObject);
            }
        }
        

    }

    private void SetRowEnemy()
    {
        frontEnemies.Clear();

        for(int i = 0; i < armies.Count; i++)
        {
            for(int j = 0; j < armies[i].GetEnemies().Count; j++)
            {
                if (armies[i].GetEnemies()[j].IsInFront() && armies[i].GetEnemies()[j].type != TYPE_ENEMY.MOUCHE)
                {
                    frontEnemies.Add(armies[i].GetEnemies()[j]);
                }
            }
        }
    }

    private void InstanceProj()
    {

        if (frontEnemies.Count > 0)
        {
            int i = Random.Range(0, frontEnemies.Count);

            if (frontEnemies[i].damage != 0)
            {

                Rigidbody2D projectile;

                if (frontEnemies[i].type == TYPE_ENEMY.MAI || frontEnemies[i].type == TYPE_ENEMY.SANTEN || frontEnemies[i].type == TYPE_ENEMY.ECHO || frontEnemies[i].type == TYPE_ENEMY.NARUTO)
                {
                    projectile = Instantiate(prefab, frontEnemies[i].spawnWeapon.position, prefab.transform.rotation);
                    gameObject.GetComponent<AudioSource>().Play();
                    projectile.gameObject.GetComponent<Projectile>().instantiater = frontEnemies[i];

                }
                else if (frontEnemies[i].type == TYPE_ENEMY.BOOMER)
                {
                    Instantiate(boomerShootPrefab, frontEnemies[i].spawnWeapon.position, boomerShootPrefab.transform.rotation);

                    projectile = boomerShootPrefab.transform.Find("ProjectileEnemy").GetComponent<Rigidbody2D>();
                    projectile.gameObject.GetComponent<Projectile>().instantiater = frontEnemies[i];

                    Rigidbody2D projectile2 = boomerShootPrefab.transform.Find("ProjectileEnemy2").GetComponent<Rigidbody2D>();
                    projectile2.gameObject.GetComponent<Projectile>().instantiater = frontEnemies[i];

                    gameObject.GetComponent<AudioSource>().Play();
                }
                //else if (frontEnemies[i].type == TYPE_ENEMY.NARUTO)
                //{
                //    Vector3 direction = (prefab.transform.position - gameManager.player.transform.position);
                //    //direction.x = 0;
                //    //direction.y = 0;

                //    projectile = Instantiate(prefab, frontEnemies[i].spawnWeapon.position, Quaternion.LookRotation(direction));
                //    gameObject.GetComponent<AudioSource>().Play();
                //    projectile.gameObject.GetComponent<Projectile>().instantiater = frontEnemies[i];
                //}

            }
        }
        

    }
}
