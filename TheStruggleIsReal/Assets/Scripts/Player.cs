using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject energyBar;
    public float moveSpeed = 6.0f; // Default move speed

    private Rigidbody2D rgb2d; // Used for moving the charcter
    private Animator anim;

    // For later use
    //private Animator animator;

    // Use this for initialization
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 shootPosition;

        if(Input.GetKeyDown("right"))
        {
            shootPosition = new Vector3(transform.position.x + 0.5f, transform.position.y, 0.0f);    
            GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
            Projectile eBarProjectile = eBar.GetComponent<Projectile>();
            eBarProjectile.moveDirection = 1;
        }

        if (Input.GetKeyDown("left"))
        {
            shootPosition = new Vector3(transform.position.x - 0.5f, transform.position.y, 0.0f);
            GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
            Projectile eBarProjectile = eBar.GetComponent<Projectile>();
            eBarProjectile.moveDirection = -1;
        }

        if (Input.GetKeyDown("up"))
        {
            shootPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f);
            GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
            Projectile eBarProjectile = eBar.GetComponent<Projectile>();
            eBarProjectile.moveDirection = 2;
        }

        if (Input.GetKeyDown("down"))
        {
            shootPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, 0.0f);
            GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
            Projectile eBarProjectile = eBar.GetComponent<Projectile>();
            eBarProjectile.moveDirection = -2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horzMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        rgb2d.velocity = new Vector2(horzMove * moveSpeed, vertMove * moveSpeed);

        if(Input.GetButtonDown("Horizontal"))
        {
            if(horzMove > 0)
            anim.SetBool("walkright", true);

            if (horzMove == 0)
                anim.SetBool("walkright", false);
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            anim.SetBool("walkright", false);
        }
    }
}
