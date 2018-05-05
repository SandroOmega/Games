using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPhysics : MonoBehaviour {

    float delta = 0.001f;
    float ShellRadius = 0.01f;
    protected Vector2 Velocity;
    protected Vector2 TargetVelocity;
    protected Rigidbody2D rigidbody;
    protected ContactFilter2D filter2D;
    protected RaycastHit2D[] hit2Ds = new RaycastHit2D[10];
    protected List<RaycastHit2D> raycastHitsList = new List<RaycastHit2D>(10);
    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();   
    }

    void Start()
    {
        filter2D.useTriggers = false;
        filter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        filter2D.useLayerMask = true;
    }

    protected virtual void ComputeVelocity()
    {

    }	
	// Update is called once per frame
	void Update () {
        ComputeVelocity();	
	}

    private void FixedUpdate()
    {
        Velocity = TargetVelocity * Time.deltaTime;
        Movement(Velocity);
    }

    void Movement(Vector2 Move)
    {
        float distance = Move.magnitude;

        if (distance > delta)
        {
            int count = rigidbody.Cast(Move, filter2D, hit2Ds, distance + ShellRadius);
            raycastHitsList.Clear();
            for (int i = 0; i < count; i++)
            {
                raycastHitsList.Add(hit2Ds[i]);
            }

            foreach (RaycastHit2D ray in raycastHitsList)
            {
                Vector2 currentNormal = ray.normal;


                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = ray.distance-ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rigidbody.position = rigidbody.position + Move.normalized*distance;

    }
}
