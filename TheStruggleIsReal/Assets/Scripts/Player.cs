using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float moveSpeed = 6.0f; // Default move speed
    private Rigidbody2D rgb2d; // Used for moving the charcter

    // For later use
    //private Animator animator;

    // Use this for initialization
	void Start () {
        rgb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float horzMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        rgb2d.velocity = new Vector2(horzMove * moveSpeed, vertMove * moveSpeed);
    }
}
