using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MyPhysics {
    [SerializeField]
    private float Acceleration=2f;
    [SerializeField]
    private float MaxSpeed = 25f;
    [SerializeField]
    private float TakeOffSpeed = 5f;
    [SerializeField]
    float Speed;
    // Use this for initialization
    void Start () {
		
	}

    protected override void ComputeVelocity()
    {
        int q=0;
        Vector2 move = Vector2.zero;
        if (isGround)
        {
            if (Input.GetAxis("Horizontal")!=0)
            {
                if ((Speed < MaxSpeed)&&(Speed>-MaxSpeed))
                {
                    Speed = Speed + Acceleration * Input.GetAxis("Horizontal")*Time.deltaTime;
                    q = (int)Input.GetAxis("Horizontal");
                }
                else
                {
                   
                    
                    Speed = MaxSpeed*q;
                }
            }
            else
            {
                if (Speed != 0)
                {
                    Speed = Speed * 0.3f;
                }
            }
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
        }
        targetVelocity.x = Speed;

    }

    // Update is called once per frame

}
