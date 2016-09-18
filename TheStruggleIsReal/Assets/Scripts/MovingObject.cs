using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

    public float moveSpeed = 6.0f; // Default move speed
    public LayerMask blockingLayer;

    private PolygonCollider2D boxCollider;
    private Rigidbody2D rb2D;

	// Use this for initialization
	protected virtual void Start () {

        boxCollider = GetComponent<PolygonCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
	}

    protected void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    protected bool Move (Vector2 dir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + dir;

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            SmoothMovement(dir);
            return true;
        }

        return false;
    }

    protected void SmoothMovement (Vector2 dir) {

        rb2D.velocity = dir * moveSpeed;
 
        
    }

    protected virtual void AttemptMove<T>(Vector2 dir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(dir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if(!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // It is object tagged with TagB
            rb2D.velocity = Vector2.zero;
        }
    }

    protected abstract void OnCantMove <T> (T component)
        where T : Component;

}
