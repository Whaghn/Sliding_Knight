using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScript : MonoBehaviour
{
    public GameObject myGameObject;
    public GameObject Player;

    void FixedUpdate()
    {
        myGameObject.transform.position = (new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z));
    }


}
