﻿using UnityEngine;
using System.Collections;

public class EnemyRanged : MovingObject
{
    public int playerDamage = 20;                            //The amount of hit points to subtract from the player when attacking.

    //private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
    private Transform target;                           //Transform to attempt to move toward each turn.
    Renderer rend;
    private Rigidbody2D rb2D;


    //Start overrides the virtual Start function of the base class.
    protected override void Start()
    {

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


    //FixedUpdate is called each frame
    public void FixedUpdate()
    {
        rend = GetComponent<Renderer>();
        if (rend.isVisible)
        {
            //Set the direction for enemy
            Vector2 dir = target.transform.position - this.transform.position;
            dir.Normalize();

            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player

            AttemptMove<Player>(dir);
        }

    }


    //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
    //and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
    protected override void OnCantMove<T>(T component)
    {
        if (component.GetComponent<Collider2D>().tag == "Player")
        {
            //Declare hitPlayer and set it to equal the encountered component.
            Player hitPlayer = component as Player;

            //Call the loseHP function of hitPlayer passing it playerDamage, the amount of HP to be subtracted.
            //hitPlayer.loseHP(playerDamage);
            hitPlayer.DamagePlayer(playerDamage);
            //Set the attack trigger of animator to trigger Enemy attack animation.
            //animator.SetTrigger("enemyAttack");

        }


    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // It is object tagged with TagB
            //System.Random rand = new System.Random();
            //int x, y;
            //x = (int)rb2D.velocity.x;
            //y = (int)rb2D.velocity.y;

            rb2D.velocity.Set(0, 0);

            //if (rand.Next()%2 == 0)
            //    rb2D.velocity = new Vector2(rb2D.velocity.x, -rb2D.velocity.y);
            //else
            //    rb2D.velocity = new Vector2(rb2D.velocity.y, -rb2D.velocity.x);
        }
    }

}