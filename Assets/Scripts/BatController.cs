using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : FlyingPhysics {
    public float MaxSpeed=10;
    

	// Use this for initialization
	void Start () {
		
	}

    protected override void ComputeVelocity()
    {
        Vector2 Move;
        Move.x = Input.GetAxis("Horizontal");
        Move.y = Input.GetAxis("Vertical");
        
        TargetVelocity = Move * MaxSpeed;
    }

   
    
}
