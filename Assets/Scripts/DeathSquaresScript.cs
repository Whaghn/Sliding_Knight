using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSquaresScript : MonoBehaviour
{

    //public SpriteRenderer mySpriteRenderer;

    public GameObject AlivePlayer;
    private AliveScript myAliveScript;
    public Rigidbody2D myRigidBody2D;
    public bool StoredSpeed;
    public bool SpeedAplied;

    void Start()
    {
        // Change color to player color (find solution)

        // mySpriteRenderer.color.b.Equals(1);
    }

    void Update()
    {
        // Add player speed before death to squares
        myAliveScript = AlivePlayer.GetComponent<AliveScript>();
        
        if (myRigidBody2D.velocity.x != 0f || myRigidBody2D.velocity.y != 0f)
        {
            StoredSpeed = true;
        }

        if (StoredSpeed && !SpeedAplied)
        {
            ApllySpeed();
            SpeedAplied = true;
        }
    }

    private void FixedUpdate()
    {
        //if (StoredSpeed)
        //{
            //myRigidBody2D.velocity = myAliveScript.Speed;
        //}
    }

    private void ApllySpeed()
    {
        myRigidBody2D.velocity = myAliveScript.Speed;
    }
}
