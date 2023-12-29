using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float pushForce = 5f; 

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        if (rb2D == null)
        {
            rb2D = gameObject.AddComponent<Rigidbody2D>();
        }

        rb2D.mass = 10f; 
        rb2D.gravityScale = 0.7f; 
        rb2D.drag = 5f; 
        rb2D.angularDrag = 5f; 

        
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider2D>(); 
        }
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            rb2D.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
        }
    }
}
