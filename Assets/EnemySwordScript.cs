using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody2D;
    public Rigidbody2D LeftAttackHitbox;
    public Rigidbody2D RightAttackHitbox;

    public GameObject AlivePlayer;
    public GameObject FinishScreen;
    public GameObject GameOverScreen;
    public GameObject EscapeScreen;

    public float SwingStrength;
    private float PlayerInRangeLeft;
    private float PlayerInRangeRight;

    public bool InMenu;

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (FinishScreen.activeSelf || GameOverScreen.activeSelf || EscapeScreen.activeSelf)
        {
            InMenu = true;
        }

        if (FinishScreen.activeSelf == false && GameOverScreen.activeSelf == false && EscapeScreen.activeSelf == false)
        {
            InMenu = false;
        }

        //Swing
        //if ( && !InMenu)
        {
            //myRigidBody2D.AddTorque(Swing * 1000 * SwingStrength);
        }


    }
}
