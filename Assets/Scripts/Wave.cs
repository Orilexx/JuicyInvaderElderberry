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

    List<EnemyController> enemies;

    private void Start()
    {
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
            Instantiate(prefab, frontEnemies[i].spawnWeapon.position, prefab.transform.rotation);

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
