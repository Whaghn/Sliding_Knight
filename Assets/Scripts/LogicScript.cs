using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    // Screen Objects
    public GameObject GameOverScreen;
    public GameObject FinishScreen;
    public GameObject EscapeScreen;

    // Player Objects
    public GameObject Alive;
    public GameObject Sword;
    public GameObject Dead;
    public GameObject Player;

    // Checks (Public for Testing)
    public bool DespawnAlivePlayer; // If true alive player despawns on death
    public bool DespawnPlayerWeapon; // If true weapon dewspawns on death
    public bool SpawnDeathSquares; // if true spawn squares on death
    public bool InScreen;
    public bool IsPaused;
    public bool GameWasOver;
    public bool GameWasWon;

    //private float EscapeInput;
    private float PreviousTimeScale;
    public int EscapeCounter = 0;

    // Load the scene again
   public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Continue();
    }

    private void Update()
    {
        // Checks if a screen is active
        if ((FinishScreen.activeSelf || GameOverScreen.activeSelf || EscapeScreen.activeSelf) && !InScreen)
        {
            InScreen = true;
        }

        //Checks if all screens are inactive
        if ((!FinishScreen.activeSelf && !GameOverScreen.activeSelf && !EscapeScreen.activeSelf) && InScreen)
        {
            InScreen = false;
        }

        if (!IsPaused && Time.timeScale > 0f)
        {
            PreviousTimeScale = Time.timeScale;
        }

        if (Input.GetButtonDown("Escape"))
        {
            if (!IsPaused)
            {
                Pause();
                EscapeScreen.SetActive(true);
            }

            if (IsPaused && EscapeCounter == 1)
            {
                Continue();
            }

            EscapeCounter += 1;

            if (EscapeCounter >= 3)
            {
                EscapeCounter = 0;
            }

            Debug.Log("Pressed Escape");
        }

        if (Input.GetButtonDown("Enter"))
        {
            if (EscapeScreen.activeSelf || FinishScreen.activeSelf)
            {
                Continue();
            }

            if (GameOverScreen.activeSelf)
            {
                RestartGame();
            }
        }
    }

    // What happens when the player loses or dies
    public void GameOver()
    {
        // Show game over screen
        GameOverScreen.SetActive(true);
        
        FinishScreen.SetActive(false);
        
        //  Despawn player if  bool is true
        if (DespawnAlivePlayer)
        {
            Alive.SetActive(false);
        }

        // Despawn weapon if bool is true
        if (DespawnPlayerWeapon)
        {
            Sword.SetActive(false);
        }

        // Spawn death squares "particle effect" if bool is true
        if (SpawnDeathSquares)
        {
            Dead.SetActive(true);
        }
    }

    public void Revive()
    {
        GameOverScreen.SetActive(false);

        // Despawn death squares "particle effect" if bool is true (should come first to prevent unintended collison with player)
        if (SpawnDeathSquares)
        {
            //Dead.SetActive(true);
        }

        // Spawn player if  bool is true
        if (DespawnAlivePlayer)
        {
            //Alive.SetActive(true);
        }

        // Spawn weapon if bool is true
        if (DespawnPlayerWeapon)
        {
            //Sword.SetActive(false);
        }

        if (!DespawnPlayerWeapon)
        {
            Sword.SetActive(false);
        }

        Player.SetActive(true);
    }

    // What happens when the player finishes or wins a level
    public void Finish()
    {
        // Shows finish Screen
        FinishScreen.SetActive(true);
        Pause();
    }

    // Closes the screens to continue game
    public void Continue()
    {
        FinishScreen.SetActive(false);
        EscapeScreen.SetActive(false);
        Time.timeScale = PreviousTimeScale;
        IsPaused = false;
        EscapeCounter += 1;

        if (EscapeCounter >= 3)
        {
            EscapeCounter = 0;
        }

        if (GameWasOver)
        {
            GameOverScreen.SetActive(true);
            GameWasOver = false;
        }

        if (GameWasWon)
        {
            FinishScreen.SetActive(true);
            GameWasWon = false;
        }
    }

    public void Pause()
    {
        if (!IsPaused)
        {
            Time.timeScale = 0;
            IsPaused = true;
            
            if (GameOverScreen.activeSelf)
            {
                GameOverScreen.SetActive(false);
                GameWasWon = true;
            }

            if (FinishScreen.activeSelf)
            {
                FinishScreen.SetActive(false);
                GameWasWon = true;
            }

            EscapeCounter += 1;
        }
    }
}