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


    private void Start()
    {
        actualCooldown = shootCooldown;
        score = 0;
        scoreText.text = "Score : " + score;
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
                actualCooldown = 0;
            }
        }

        actualCooldown += Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
