using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MyPhysics
{
    [SerializeField]
    private float MaxSpeed = 10f;
    [SerializeField]
    private float TakeOffSpeed = 4f;



    // Use this for initialization
    void Start()
    {

    }

    protected override void ComputeVelocity()
    {
        
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && isGround)
            {
                Velocity.y = TakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (Velocity.y > 0)
                {
                    Velocity.y = Velocity.y * 0.5f;
                }
            }
            targetVelocity = move * MaxSpeed;
       
    }

    // Update is called once per frame
}
