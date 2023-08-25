using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveScript : MonoBehaviour
{
    // Internal Modules
    public Rigidbody2D myRigidBody2d;
    public AudioSource JumpSound;

    // External Scripts to Get Variables From
    private LogicScript myLogicScript;
    private PlayerScript myPlayerScript;

    // Game Objects
    public GameObject FinishScreen;
    public GameObject GameOverScreen;
    public GameObject EscapeScreen;
    public GameObject Logic;
    public GameObject Player;

    // Hitboxes
    public GameObject JumpHitbox;
    public GameObject WallJumpHitbox;
    public GameObject HazardHitbox;
    private BoxCollider2D JumpCollider2D;
    private BoxCollider2D WallJumpCollider2D;
    private BoxCollider2D HazardCollider2D;

    // Adjustable Values
    public float JumpStrenght; // Multiplies the jump force
    public float MoveSpeed; // Multiplies the walking force
    public float FlySpeed; // Multiplies horizontal speed
    public float FlyDrag; // used change the "stopping power" in fly mode
    public float HorizontalSprintSpeed;
    public float VerticalSprintSpeed;

    // Checks
    private bool GameOver; // If true player has lost or died
    private bool Won; // If true player has finished the level
    private bool LevelFinished; // Used run finish function only once
    private bool InScreen; // If true a screen is active
    private bool InAir;// If true player is not touching the ground
    private bool DevMode;

    // Modes
    public bool WallJumpMode; // if true can jump on anything that has collision
    public bool InvincibleMode;
    public bool FlyMode; // If true activates fly mode and allows flying sprint
    public bool GodMode; // if true activates god mode (Invincinble and All Jump)

    // Inputs
    private float HorizontalInput; // Stores the input value between 1 and -1 from the horizontal input axis
    private float JumpInput; // Stores the input value between 0 and 1 from the jump input axis
    private float VerticalInput; // Stores the input value between 1 and -1 from the vertical input axis (fly mode)
    private float SprintInput;

    // Containers
    private float GravityScale; // Stores the gravity scale value
    private float LinearDrag; // Stores the linear drag value
    public Vector2 Speed; // Stores the speed vector of the player rigid body

    // Counters
    private int WallJumpModeCounter = 0;
    private int InvincibleModeCounter = 0;
    private int FlyModeCounter = 0;
    private int GodModeCounter = 0;

    private void Start()
    {
        GravityScale = myRigidBody2d.gravityScale; // Stores the gravity scale of the player when the level loads to revert from flying mode
        LinearDrag = myRigidBody2d.drag; // Stores the linear drag of the player when the level loads to revert from flying mode

        JumpCollider2D = JumpHitbox.GetComponent<BoxCollider2D>();
        WallJumpCollider2D = WallJumpHitbox.GetComponent<BoxCollider2D>();
        HazardCollider2D = HazardHitbox.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Checks if user is pressing the keyboard keys for movement
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        JumpInput = Input.GetAxisRaw("Jump");
        VerticalInput = Input.GetAxisRaw("Vertical");
        SprintInput = Input.GetAxisRaw("Sprint");

        // Runs game over function if player lost or died and not in god mode and not in a screen
        if (GameOver && !InvincibleMode && !InScreen)
        {
            myLogicScript.GameOver();
        }

        // Runs finish function if player has won and not in god and not in a screen
        if (Won && !LevelFinished && !InvincibleMode && !InScreen)
        {
            myLogicScript.Finish();
            LevelFinished = true;
        }

        if (Input.GetButtonDown("Game Over"))
        {
            if (!GameOver)
            {
                GameOver = true;
            }
        }

        if (Input.GetButtonDown("Wall Jump Mode") && DevMode)
        {
            if (!WallJumpMode)
            {
                WallJumpMode = true;
            }

            if (WallJumpMode && WallJumpModeCounter == 1)
            {
                WallJumpMode = false;
            }

            WallJumpModeCounter += 1;

            if (WallJumpModeCounter == 2)
            {
                WallJumpModeCounter = 0;
            }
        }

        if (Input.GetButtonDown("Invincible Mode") && DevMode)
        {
            if (!InvincibleMode)
            {
                InvincibleMode = true;
            }

            if (InvincibleMode && InvincibleModeCounter == 1)
            {
                InvincibleMode = false;
            }

            InvincibleModeCounter += 1;

            if (InvincibleModeCounter == 2)
            {
                InvincibleModeCounter = 0;
            }
        }

        if (Input.GetButtonDown("Fly Mode") && DevMode)
        {

            if (!FlyMode)
            {
                FlyMode = true;
                myRigidBody2d.gravityScale = 0;
                myRigidBody2d.drag = FlyDrag;
            }

            if (FlyMode && FlyModeCounter == 1)
            {
                FlyMode = false;
                myRigidBody2d.gravityScale = GravityScale;
                myRigidBody2d.drag = LinearDrag;
            }

            FlyModeCounter += 1;

            if (FlyModeCounter == 2)
            {
                FlyModeCounter = 0;
            }
        }

        if (Input.GetButtonDown("God Mode") && DevMode)
        {
            if (!GodMode)
            {
                GodMode = true;
            }

            if (GodMode && GodModeCounter == 1)
            {
                GodMode = false;
            }

            GodModeCounter += 1;

            if (GodModeCounter == 2)
            {
                GodModeCounter = 0;
            }
        }

        myLogicScript = Logic.GetComponent<LogicScript>();
        InScreen = myLogicScript.InScreen;

        myPlayerScript = Player.GetComponent<PlayerScript>();
        DevMode = myPlayerScript.DevMode;
    }

    private void FixedUpdate()
    {
        // Walk according to horizontal movement inputs and if not in a screen and not flying
        if ((HorizontalInput < 0f || HorizontalInput > 0f) && !myLogicScript.InScreen && !FlyMode)
        {
            myRigidBody2d.AddForce(new Vector2(HorizontalInput * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        // Jump according to jump inputs and if not in a screen and not flying
        if (!InAir && JumpInput > 0f && !InScreen && !FlyMode)
        {
            myRigidBody2d.AddForce(new Vector2(0f, JumpInput * 100 * JumpStrenght), ForceMode2D.Impulse);
            JumpSound.Play(); // Plays the jumping sound
        }

        // Flyies vertically if not in a screen and flying is active
        if ((VerticalInput < 0f || VerticalInput > 0f) && !InScreen && FlyMode)
        {
            if (SprintInput == 0)
            {
                myRigidBody2d.velocity = new Vector2(myRigidBody2d.velocity.x, VerticalInput * FlySpeed);
            }

            if (SprintInput > 0f)
            {
                myRigidBody2d.velocity = new Vector2(myRigidBody2d.velocity.x, VerticalInput * FlySpeed * SprintInput * VerticalSprintSpeed);
            }
        }

        // Flyies horizontally if not in a screen and flying is active
        if ((HorizontalInput < 0f || HorizontalInput > 0f) && !InScreen && FlyMode)
        {
            if (SprintInput == 0)
            {
                myRigidBody2d.velocity = new Vector2(HorizontalInput * FlySpeed, myRigidBody2d.velocity.y);
            }

            if (SprintInput > 0f)
            {
                myRigidBody2d.velocity = new Vector2(HorizontalInput * FlySpeed * SprintInput * HorizontalSprintSpeed, myRigidBody2d.velocity.y);
            }

        }

        // Stores latest speed vector
        Speed = myRigidBody2d.velocity;
    }

    // Collision functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!WallJumpMode)
        {
            // Checks if player is touching the ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                InAir = false;
                //Debug.Log("In Ground");
            }
        }

        if (WallJumpMode)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                InAir = false;
            }
        }

        // Functions that run only if god mode is inactive
        if (!InvincibleMode)
        {
            // Checks if player hit a spike and activates game over state
            if (collision.gameObject.CompareTag("Spike"))
            {
                GameOver = true;
            }
        
            // Checks if the level has already been finished
            if (LevelFinished == false)
            {
                // Checks if player entered the finish hitbox
                if (collision.gameObject.CompareTag("Finish"))
                {
                    Won = true;
                }
            }
        }
    }

    // Exit collision functions
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!WallJumpMode)
        {
            // Checks if player exited the ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                InAir = true;
                //Debug.Log("In Air");
            }
        }

        if (WallJumpMode)
        {
            if (!collision.gameObject)
            {
                InAir = true;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (WallJump)
    //    {
    //        if (collision.gameObject.CompareTag("Ground"))
    //        {
    //            InAir = false;
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (WallJump)
    //    {
    //        if (!collision.gameObject)
    //        {
    //            InAir = true;
    //        }
    //    }
    //}
}
