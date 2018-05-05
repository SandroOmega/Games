using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : MonoBehaviour {

    [SerializeField]
    private float gravityForce=1f;
    private float minGroundNormalY = 0f; 

    protected Vector2 targetVelocity;
    [SerializeField]
    protected bool isGround;
    protected Vector2 groundNormal;
    protected Rigidbody2D Rgdb2d;
    protected Vector2 Velocity;
    protected ContactFilter2D filter2D;
    protected RaycastHit2D[] hit2Ds = new RaycastHit2D[10];
    protected List<RaycastHit2D> raycastHitsList = new List<RaycastHit2D>(10);

    public float Gravity
    {
        set
        {

        }
        get
        {
            return 0;
        }
    }
    
    protected const float delta = 0.0001f;
    protected const float shellRadius = 0.01f;

    private void Awake()
    {
        targetVelocity = Vector2.zero;
        Velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        Rgdb2d = GetComponent<Rigidbody2D>();
        targetVelocity = Vector2.zero;
        Velocity = Vector2.zero;
    }
    // Use this for initialization
    void Start () {
        filter2D.useTriggers = false;
        filter2D.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        filter2D.useLayerMask = true;
    }
	
	// Update is called once per frame
	void Update () {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}


    void FixedUpdate()
    {
        Velocity += gravityForce * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x;

        isGround = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;

        Vector2 MoveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = MoveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    protected virtual void ComputeVelocity()
    {

    }

    void Movement(Vector2 Move, bool yMovement)
    {
        float distance = Move.magnitude;
        if (distance > delta)
        {
            int count = Rgdb2d.Cast(Move, filter2D, hit2Ds, distance+shellRadius);
            raycastHitsList.Clear();
            for (int i = 0; i < count; i++)
            {
                raycastHitsList.Add(hit2Ds[i]);
            }

            foreach (RaycastHit2D ray in raycastHitsList)
            {
                Vector2 currentNormal = ray.normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    //Debug.Log(currentNormal.y.ToString());
                    isGround = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                
                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = ray.distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        Rgdb2d.position = Rgdb2d.position + Move.normalized * distance;
    }

}
