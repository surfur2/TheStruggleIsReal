using UnityEngine;
using System.Collections;

public class ProjectileEnemy : Projectile {

    protected override void Start()
    {
        moveSpeed = 3f;
        base.Start();
    }
    protected override void Update()
    {
        if (moveDirection == 1.0f)
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);

        if (moveDirection == -1f)
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);

        if (moveDirection == 2f)
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        if (moveDirection == -2f)
            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);

        if (moveDirection == 1.5f)
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        if (moveDirection == -1.5f)
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);

        if (moveDirection == 3f)
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);

        if (moveDirection == -3f)
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);
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
