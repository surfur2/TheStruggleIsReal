using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject energyBar;
    public float moveSpeed = 6.0f; // Default move speed
    public float initialHealth = 100.0f;
    public float rateOfFire = 2.0f;
    public Slider playerHealthSlider;

    private Rigidbody2D rgb2d; // Used for moving the charcter
    [SerializeField]
    private float currentHealth; // Intial HP
    private Animator anim;
    private float nextFire = 0;
    // For later use
    //private Animator animator;

    // Use this for initialization
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = initialHealth;
        playerHealthSlider.maxValue = initialHealth;
    }

    void Update()
    {
        Vector3 shootPosition;
        
        if (Time.time > nextFire)
        {
            if (Input.GetKey("right"))
            {
                shootPosition = new Vector3(transform.position.x + 0.5f, transform.position.y, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = 1;

                nextFire = Time.time + (1 / rateOfFire);
            }

            if (Input.GetKey("left"))
            {
                shootPosition = new Vector3(transform.position.x - 0.5f, transform.position.y, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = -1;

                nextFire = Time.time + (1 / rateOfFire);
            }

            if (Input.GetKey("up"))
            {
                shootPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = 2;

                nextFire = Time.time + (1 / rateOfFire);
            }

            if (Input.GetKey("down"))
            {
                shootPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = -2;

                nextFire = Time.time + (1 / rateOfFire);
            }
        }

        playerHealthSlider.value = currentHealth;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        anim.SetFloat("MoveX", inputX);
        anim.SetFloat("MoveY", inputY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horzMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        rgb2d.velocity = new Vector2(horzMove * moveSpeed, vertMove * moveSpeed);

        if (horzMove != 0 || vertMove != 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    public void DamagePlayer(float hitPoints)
    {
        currentHealth += hitPoints;

        if (currentHealth <= 0.0f)
        {
            Debug.Log("PlayerDead");
        }
    }

}
