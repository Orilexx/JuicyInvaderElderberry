using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
