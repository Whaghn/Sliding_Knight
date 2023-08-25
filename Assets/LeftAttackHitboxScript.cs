using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAttackHitboxScript : MonoBehaviour
{

    public GameObject EnemySword;
    public bool PlayerInLeftRange;

    private void FixedUpdate()
    {
        if (PlayerInLeftRange)
        {
           // EnemySword.rigidbody (would need a get component)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInLeftRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInLeftRange = false;
        }
    }

}

