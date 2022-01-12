using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GameManager gameManager;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("OUI" + gameObject.transform.TransformPoint(gameObject.transform.position));
        Debug.Log("NON" + gameManager.player.transform.position);
        Vector3 targetDirection = gameManager.player.transform.position - gameObject.transform.TransformPoint(gameObject.transform.position);

        float singleStep = speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.TransformPoint(gameObject.transform.position), targetDirection, singleStep, 0.0f);
        //newDirection.x = 0;
        //newDirection.y = 180;

        Vector2 lookDir =  gameObject.transform.position - gameManager.player.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //gameObject.transform.LookAt(gameObject.transform);
    }
}
