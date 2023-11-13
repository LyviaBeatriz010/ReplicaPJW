using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
   
    public int Speed;
    private Rigidbody2D rig;
    private Animator anim;
    private bool iswalking;

    private bool isclimbing;
   
       
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
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


        Vector2 movement = new Vector2(H,V);
        rig.velocity = new Vector2(movement.x * Speed, movement.y * Speed);
       
       iswalking = (Mathf.Abs(H) > 0f );
    
       anim.SetBool("IsWalking", iswalking);

       isclimbing = (  Mathf.Abs(V) > 0f);

        anim.SetBool("IsClimbing", isclimbing);
    }


   


}



