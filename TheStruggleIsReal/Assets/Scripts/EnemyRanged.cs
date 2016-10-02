using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy
{
   // public int playerDamage = 20;                            //The amount of hit points to subtract from the player when attacking.

    //private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
    private Transform target;                           //Transform to attempt to move toward each turn.
    public GameObject energyBar;

    private float nextFire = 0;
    public float rateOfFire;


    //Start overrides the virtual Start function of the base class.
    protected override void Start()
    {
        rateOfFire = 1f;
        hp = 1;
        playerDamage = 20;
        //Get and store a reference to the attached Animator component.
        //animator = GetComponent<Animator>();

        //Find the Player GameObject using it's tag and store a reference to its transform component.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        setMoveSpeed(1.2f);

        //Call the start function of our base class MovingObject.
        base.Start();
    }


    //Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns.
    //See comments in MovingObject for more on how base AttemptMove function works.
    protected override void AttemptMove<T>(Vector2 dir)
    {

        //Call the AttemptMove function from MovingObject.
        base.AttemptMove<T>(dir);

    }

    void Update()
    {
        Vector2 chasingVector = (target.transform.position - this.transform.position);
        if (chasingVector.magnitude < 4)
        {
            Vector3 shootPosition;
            RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position, chasingVector, chaseRadius);
            bool shouldShoot = false;

            foreach(RaycastHit2D hit in hitList)
            {
                if (hit.collider.tag == "Wall")
                    break;

                if (hit.collider.tag == "Player")
                    shouldShoot = true;
            }

            if (Time.time > nextFire && shouldShoot)
            {
                shootPosition = this.transform.position;
                GameObject eBar = Instantiate(energyBar, shootPosition, Quaternion.identity) as GameObject;
                ProjectileEnemy eBarProjectile = eBar.GetComponent<ProjectileEnemy>();
                chasingVector.Normalize();
                eBarProjectile.direction = chasingVector;
                //float ratio = chasingVector.y / chasingVector.x;
                //if (ratio < 1 && chasingVector.x > 0) 
                //{
                //    eBarProjectile.moveDirection = 1f;
                //}
                //else if (ratio < 1 && chasingVector.x < 0)
                //{
                //    eBarProjectile.moveDirection = -1f;
                //}
                //else if (ratio > 1 && chasingVector.x > 0)
                //{
                //    eBarProjectile.moveDirection = 2f;
                //}
                //else if (ratio > 1 && chasingVector.x < 0)
                //{
                //    eBarProjectile.moveDirection = -2f;
                //}
                //else if (ratio == 1 && chasingVector.x > 0)
                //{
                //    eBarProjectile.moveDirection = 1.5f;
                //}
                //else if (ratio == 1 && chasingVector.x < 0)
                //{
                //    eBarProjectile.moveDirection = -3f;
                //}
                //else if (ratio == -1 && chasingVector.x > 0)
                //{
                //    eBarProjectile.moveDirection = -3f;
                //}
                //else if (ratio == -1 && chasingVector.x > 0)
                //{
                //    eBarProjectile.moveDirection = 3f;
                //}
                //if (chasingVector.x > 0)
                //{
                //    if (chasingVector.y == 0)
                //    {
                //        eBarProjectile.moveDirection = 1.0f;
                //    }
                //    else if (chasingVector.y > 0)
                //    {
                //        eBarProjectile.moveDirection = 1.5f;
                //    }
                //    else if (chasingVector.y < 0)
                //    {
                //        eBarProjectile.moveDirection = 3f;
                //    }
                //}
                //else if (chasingVector.x < 0)
                //{
                //    if (chasingVector.y == 0)
                //    {
                //        eBarProjectile.moveDirection = -1f;
                //    }
                //    else if (chasingVector.y > 0)
                //    {
                //        eBarProjectile.moveDirection = -1.5f;
                //    }
                //    else if (chasingVector.y < 0)
                //    {
                //        eBarProjectile.moveDirection = -3f;
                //    }
                //}
                //else if (chasingVector.x == 0)
                //{
                //    if (chasingVector.y == 0)
                //    {
                //        //Do nothing. Collision takes care of it
                //    }
                //    else if (chasingVector.y > 0)
                //    {
                //        eBarProjectile.moveDirection = 2f;
                //    }
                //    else if (chasingVector.y < 0)
                //    {
                //        eBarProjectile.moveDirection = -2f;
                //    }
                //}

                nextFire = Time.time + (1 / rateOfFire);

            }
        }
    }

    //FixedUpdate is called each frame
    protected override void FixedUpdate()
    {
        Vector2 chasingVector = (target.transform.position - this.transform.position);
        if (chasingVector.magnitude < chaseRadius && chasingVector.magnitude > 4)
        {

            chasingVector.Normalize();
            AttemptMove<Player>(chasingVector);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        

    }


    //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
    //and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player


    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.GetComponent<Collider2D>().tag == "Player")
    //    {
    //        //System.Random rand = new System.Random();

    //        //Vector2 vel = this.transform.GetComponent<Rigidbody2D>().velocity;
    //        //for (int i = 0; i < 4; i++)
    //        //{

    //        //    if (rand.Next() % 2 == 0)
    //        //        SmoothMovement((new Vector2(vel.x, -vel.y)) * 2);
    //        //    else
    //        //        SmoothMovement((new Vector2(vel.y, -vel.x)) * 2);
    //        //}
    //        //this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //    }
    //}

}
