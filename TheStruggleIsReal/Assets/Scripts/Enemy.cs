using UnityEngine;
using System.Collections;

public class Enemy : MovingObject
{
    public int playerDamage;                            //The amount of hit points to subtract from the player when attacking.
    private Collider2D enemyCollider;                         //The amount of hit points to subtract from the player when attacking.
    public int hp;                                      // The amount of hp enemy starts with
    public int chaseRadius = 7;                            // When does an enemy attempt to go after the player?
    //private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
    private Transform target;                           //Transform to attempt to move toward each turn.
    Renderer rend;


    //Start overrides the virtual Start function of the base class.
    protected override void Start()
    {

        //Get and store a reference to the attached Animator component.
        //animator = GetComponent<Animator>();

        //Find the Player GameObject using it's tag and store a reference to its transform component.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rend = GetComponent<Renderer>();

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

    protected new bool Move(Vector2 dir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + dir;

        enemyCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        enemyCollider.enabled = true;

        if (hit.transform.GetComponent<Collider2D>().tag == "Wall")
        {
            SmoothMovement(new Vector2(0, 0));
            return true;
        }
        if (hit.transform == null)
        {
            SmoothMovement(dir);
            return true;
        }

        return false;
    }


    //FixedUpdate is called each frame
    protected virtual void FixedUpdate()
    {


        // Do we have LoS on player?
        Vector2 chasingVector = (target.transform.position - this.transform.position);
        //// We do not want to hit our own collider, so we add in a small vector to prevent that
        //RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position, chasingVector, chaseRadius);
        //bool chase = false;

        //foreach(RaycastHit2D hit in hitList)
        //{
        //    if (hit.collider.tag == "Wall")
        //        break;

        //    if (hit.collider.tag == "Player")
        //        chase = true;
        //}

        //if (chase)
        //{
        //    //Set the direction for enemy
        //    chasingVector.Normalize();

        //    //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player

        //    AttemptMove<Player>(chasingVector);
        //}
        //else
        //{
        //    AttemptMove<Player>(new Vector2(0, 0));
        //}
        if (chasingVector.magnitude < chaseRadius)
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
    protected override void OnCantMove<T>(T component)
    {


    }

    public void DamageEnemy(int dmg)
    {

        hp -= dmg;

        if (hp <= 0)
        {
            Destroy(gameObject, .1f);
        }
    }

}
