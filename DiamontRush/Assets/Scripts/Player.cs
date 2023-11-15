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

        iswalking = (Mathf.Abs(H) > 0f);
        anim.SetBool("IsWalking", iswalking);

        isclimbing = areaVerdeCollider != null && areaVerdeCollider.OverlapPoint(transform.position) && !autoClimb;

        if (isclimbing && !IsGrounded())
        {
            autoClimb = true;
        }
        else
        {
            autoClimb = false;
        }

        anim.SetBool("IsClimbing", isclimbing);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }
}
