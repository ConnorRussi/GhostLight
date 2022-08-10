using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;
    public Transform placeToTeleport;

    private void Start()
    {
        player = GameObject.Find("Player");
        ghost = GameObject.Find("Ghost");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject == player)
        {
            player.transform.position = placeToTeleport.position;
          //  Debug.Log("Player Teleported to " + placeToTeleport);
        }
        else if (collision.collider.gameObject == ghost)
        {

            ghost.transform.position = placeToTeleport.position;
            //Debug.Log("Ghost Teleported to " + placeToTeleport);

        }
        else
        {
           // Debug.Log(collision.collider.gameObject.name + "Was not Teleported");
            return;
        }
    }
}
