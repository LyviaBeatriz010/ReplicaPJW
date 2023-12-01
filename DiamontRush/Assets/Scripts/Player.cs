using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public int Speed;
    private Rigidbody2D rig;
    private Animator anim;
    private bool iswalking;
    private bool isclimbing;
    private TilemapCollider2D areaVerdeCollider;

    private bool autoClimb;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        areaVerdeCollider = GameObject.FindObjectOfType<TilemapCollider2D>().gameObject.layer ==
                            LayerMask.NameToLayer("AreaVerdeTilemap") ? GameObject.FindObjectOfType<TilemapCollider2D>() : null;
    }

    void Update()
    {
        Move();
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

        
        if(IsGrounded())
        {
            iswalking = (Mathf.Abs(H) > 0f);
            anim.SetBool("IsWalking", iswalking);
        }
        else
        {
            iswalking = false;
            anim.SetBool("IsWalking", iswalking);
        }
        
        isclimbing = areaVerdeCollider != null && areaVerdeCollider.OverlapPoint(transform.position) && !autoClimb;
        
        if (isclimbing && !IsGrounded())
        {
            Debug.Log("to entrando");
            autoClimb = true;

            if (V > 0f)
            {
                anim.SetBool("IsClimbing", false);
                anim.SetBool("ClimbingUp", true);
            }
            else if (V < 0f)
            {
                anim.SetBool("IsClimbing", false);
                anim.SetBool("ClimbingDown", true);
            }
            
            else if (Mathf.Abs(H) > 0)
            {
                anim.Play("P_escaladaL");
            
            }
            else
            {
                anim.SetBool("ClimbingUp", false);
                anim.SetBool("ClimbingDown", false);
                anim.SetBool("IsClimbing", true);
                anim.SetBool("IsClimbing", isclimbing);
            }
        }
        else
        {
            autoClimb = false;
            anim.SetBool("ClimbingUp", false);
            anim.SetBool("ClimbingDown", false);
          
        }

    }

    bool IsGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0,-1f,0));
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
    }
}
