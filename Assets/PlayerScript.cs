using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject Alive;
    public GameObject Dead;
    public GameObject Sword;
    public bool DevMode;
    public bool Revive;
    

    void Update()
    {
        if (Revive && GameOverScreen.activeSelf && DevMode && Input.GetButtonDown("Revive"))
        {
            Alive.SetActive(true);
            Dead.SetActive(false);
            Sword.SetActive(true);
            GameOverScreen.SetActive(false);
            Revive = false;
            Debug.Log("Revived");
        }

    }
}
