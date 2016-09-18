﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int moveDirection;
    public float moveSpeed = 100.0f;
    public int killDelay = 2;
    public int damage = 1;
   
    // Use this for initialization
    void Start()
    {
        Kill();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDirection == 1)
        transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);

        if (moveDirection == -1)
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);

        if (moveDirection == 2)
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        if (moveDirection == -2)
            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);
    }

    void Kill()
    {
        Destroy(gameObject, killDelay);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject, 0f);
            EnemyLonely myEnemy = other.gameObject.GetComponent<EnemyLonely>();
            myEnemy.DamageEnemy(damage);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject, 0f);
        }
    }
}
