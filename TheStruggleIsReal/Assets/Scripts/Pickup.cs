using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.DamagePlayer(-50.0f);
            Destroy(gameObject);
        }
    }
}
