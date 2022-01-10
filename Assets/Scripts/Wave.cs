using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public List<ArmyManager> armies;

    [SerializeField] Rigidbody2D prefab;

    [SerializeField] float timer = 3f;
    private float timeLeft;

    [SerializeField] List<EnemyController> frontEnemies;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        timeLeft = timer;
    }

    void Update()
    {

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = timer;

            SetRowEnemy();

            int i = Random.Range(0, frontEnemies.Count);

            if (frontEnemies[i].damage != 0)
            {
                Rigidbody2D projectile = Instantiate(prefab, frontEnemies[i].spawnWeapon.position, prefab.transform.rotation);
                projectile.gameObject.GetComponent<Projectile>().instantiater = frontEnemies[i];
            }

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
                if (armies[i].GetEnemies()[j].IsInFront())
                {
                    frontEnemies.Add(armies[i].GetEnemies()[j]);
                }
            }
        }
    }
}
