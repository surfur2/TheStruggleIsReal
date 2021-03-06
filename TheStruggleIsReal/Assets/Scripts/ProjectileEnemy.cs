﻿using UnityEngine;
using System.Collections;

public class ProjectileEnemy : Projectile {

    public Vector2 direction;

    protected override void Start()
    {
        damage = 3;
        moveSpeed = 3f;
        base.Start();
    }
    protected override void Update()
    {
        //transform.position += new Vector3(direction.x + (moveSpeed * Time.deltaTime), direction.y + (moveSpeed * Time.deltaTime), 0.0f);
        GetComponent<Rigidbody2D>().AddForce(direction * moveSpeed, 0);
        //transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);
        //if (moveDirection == 1.0f)
        //    transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);

        //if (moveDirection == -1f)
        //    transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);

        //if (moveDirection == 2f)
        //    transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        //if (moveDirection == -2f)
        //    transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);

        //if (moveDirection == 1.5f)
        //    transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        //if (moveDirection == -1.5f)
        //    transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        //if (moveDirection == 3f)
        //    transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);

        //if (moveDirection == -3f)
        //    transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject, 0f);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject, 0f);
        }
    }
}
