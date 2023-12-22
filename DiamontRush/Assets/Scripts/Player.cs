using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public int Health = 3;
    public int Speed;
    private Rigidbody2D rig;
    private Animator anim;
    private bool isInAreaVerde;

    private bool isCollidingWithPedra;
    private bool EmCimaDaPedra = false;

    private bool isPushingPedra = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameController.instance.AtualizandoVidas(Health);
    }

    void Update()
    {
        Move();
        if (isCollidingWithPedra && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            if (EmCimaDaPedra)
            {
                anim.SetInteger("transition", 1); 
            }
            else
            {
                anim.SetInteger("transition", 6);
            }
            
        }
        else if (isInAreaVerde)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                anim.SetInteger("transition", 5);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                anim.SetInteger("transition", 3);
            }
            else if (Input.GetAxis("Horizontal") != 0)
            {
                anim.SetInteger("transition", 4);
            }
            else if (!IsGrounded())
            {
                anim.SetInteger("transition", 2);
            }
        

            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f); 
            }
        
        }
        else if (!isInAreaVerde) 
        {
            if (IsGrounded())
            {
                float H = Input.GetAxis("Horizontal");
                float V = Input.GetAxis("Vertical");

                if (Mathf.Approximately(H, 0) && Mathf.Approximately(V, 0))
                {
                    anim.SetInteger("transition", 0);
                }
                else
                {
                    anim.SetInteger("transition", 1);

                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    }
                    else if (Input.GetAxis("Horizontal") < 0)
                    {
                        transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    }
                }
            }
        }
    }



    public void Damage(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            // morre
        }
    }
    
    
    
    void Move()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        if (Mathf.Abs(H) > Mathf.Abs(V))
        {
            V = 0f;
        }
        else
        {
            H = 0f;
        }

        Vector2 movement = new Vector2(H, V);

        rig.velocity = new Vector2(movement.x * Speed, movement.y * Speed);

        if (IsGrounded())
        {
            if (Mathf.Approximately(H, 0) && Mathf.Approximately(V, 0))
            {
                anim.SetInteger("transition", 0);
            }
            else
            {
                anim.SetInteger("transition", 1);
            
             if (Input.GetAxis("Horizontal") > 0)
                  {
                     transform.eulerAngles = new Vector3(0f, 0f, 0f);
                  }
            else if (Input.GetAxis("Horizontal") < 0)
                 {
                    transform.eulerAngles = new Vector3(0f, 180f, 0f); 
                 }
            }
        }
    }

    bool IsGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -1f, 0));
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
    }

   

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AreaVerde"))
        {
            isInAreaVerde = true;
        }
    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AreaVerde"))
        {
            isInAreaVerde = false;
            anim.SetInteger("transition", 0);
        }
    
    }

    
     void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pedra") && !isPushingPedra) 
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 center = collision.collider.bounds.center;
            
            isCollidingWithPedra = true;

            if(contactPoint.y > center.y && Input.GetAxis("Vertical") < 0)
            {
                EmCimaDaPedra = true;
                isPushingPedra = true; 
               

                if (Input.GetAxis("Horizontal") > 0)
                {
                    transform.eulerAngles = new Vector3 (0f, 0f, 0f);
                    
                    Debug.Log("Estou em cima da pedra e pressionando para a direita");
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    transform.eulerAngles = new Vector3 (0f, 180f, 0f);
                    
                    Debug.Log("Estou em cima da pedra e pressionando para a esquerda");
                }
                else
                {
                   
                    Debug.Log("Estou em cima da pedra, mas sem movimento horizontal");
                }
            }
            else
            {
                EmCimaDaPedra = false;
            }


            if (contactPoint.y < center.y)
            {
               // ajeitar ainda o dano 
            }
        
        
        }
    }

   
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pedra"))
        {
            isCollidingWithPedra = false;
            isPushingPedra = false; 
        }
    }   
}