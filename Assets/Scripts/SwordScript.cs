using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody2D;
    public GameObject FinishScreen;
    public GameObject GameOverScreen;
    public GameObject EscapeScreen;

    public float SwingStrength;

    public bool InMenu;

    private float Swing;

    private void Update()
    {
        // Checks if user is pressing the keyboard keys for movement
        Swing = Input.GetAxisRaw("Fire1");
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

        if ((Swing > 0f || Swing < 0f) && !InMenu)
        {
            myRigidBody2D.AddTorque(Swing * 1000 * SwingStrength);
        }
    }
}
