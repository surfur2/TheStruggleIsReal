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
    private SpriteRenderer sprRnd2d;
    [SerializeField]
    private float currentHealth; // Intial HP
    private Animator anim;
    private float nextFire = 0;
    private float iFrameDuration = 1.0f;
    public float iFrameEnd;
    public bool invulnerable = false;
    // For later use
    //private Animator animator;

    // Use this for initialization
    void Start()
    {
        sprRnd2d = GetComponent<SpriteRenderer>();
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
            if (Input.GetKey("right") && Input.GetKey("up"))
            {
                shootPosition = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = 1.5f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("left") && Input.GetKey("up"))
            {
                shootPosition = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = -1.5f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("left") && Input.GetKey("down"))
            {
                shootPosition = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = -3.0f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("right") && Input.GetKey("down"))
            {
                shootPosition = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = 3.0f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("right"))
            {
                shootPosition = new Vector3(transform.position.x + 0.5f, transform.position.y, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = 1.0f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("left"))
            {
                shootPosition = new Vector3(transform.position.x - 0.5f, transform.position.y, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = -1.0f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("down"))
            {
                shootPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = -2;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }

            else if (Input.GetKey("up"))
            {
                shootPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f);
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                Projectile eBarProjectile = eBar.GetComponent<Projectile>();
                eBarProjectile.moveDirection = 2.0f;

                nextFire = Time.time + (1 / rateOfFire);
                DamagePlayer(2);
            }
        }

        if (iFrameEnd < Time.time)
        {
            invulnerable = false;
        }

        playerHealthSlider.value = currentHealth;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        anim.SetFloat("MoveX", inputX);
        anim.SetFloat("MoveY", inputY);
    }

    IEnumerator Blink()
    {
        while (invulnerable)
        {
            sprRnd2d.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sprRnd2d.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
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

            // Set idol animation float
            if (horzMove >= 0 && vertMove >= 0)
            {
                if (horzMove > vertMove)
                {
                    anim.SetFloat("lastDirection", .75f);
                }
                else
                {
                    anim.SetFloat("lastDirection", 1.75f);
                }
            }
            else if (horzMove <= 0 && vertMove >= 0)
            {
                if (Mathf.Abs(horzMove) > vertMove)
                {
                    anim.SetFloat("lastDirection", .25f);
                }
                else
                {
                    anim.SetFloat("lastDirection", 1.75f);
                }
            }
            else if (horzMove <= 0 && vertMove <= 0)
            {
                if (Mathf.Abs(horzMove) > Mathf.Abs(vertMove))
                {
                    anim.SetFloat("lastDirection", .25f);
                }
                else
                {
                    anim.SetFloat("lastDirection", -.25f);
                }
            }
            else if (horzMove >= 0 && vertMove <= 0)
            {
                if (horzMove > Mathf.Abs(vertMove))
                {
                    anim.SetFloat("lastDirection", .75f);
                }
                else
                {
                    anim.SetFloat("lastDirection", -.25f);
                }
            }
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    public void DamagePlayer(float hitPoints)
    {

        currentHealth -= hitPoints;

        if (currentHealth <= 0.0f)
        {
            Debug.Log("PlayerDead");
        }

        if (currentHealth > initialHealth)
        {
            currentHealth = initialHealth;
        }
    }

    public void MakeInvulnerable()
    {
        invulnerable = true;
        iFrameEnd = Time.time + iFrameDuration;
        StartCoroutine(Blink());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !invulnerable)
        {
            Enemy myEnemy = other.gameObject.GetComponent<Enemy>();
            DamagePlayer(myEnemy.playerDamage);
            MakeInvulnerable();
        }
        else if (other.gameObject.tag == "Projectile" && !invulnerable)
        {
            DamagePlayer(other.gameObject.GetComponent<ProjectileEnemy>().damage);
            MakeInvulnerable();
        }
        //else if (other.gameObject.name.Contains("EnemyRanged") && !invulnerable)
        //{
        //    EnemyRanged myEnemy = other.gameObject.GetComponent<EnemyRanged>();
        //    DamagePlayer(myEnemy.playerDamage);
        //    MakeInvulnerable();
        //}
    }
}
