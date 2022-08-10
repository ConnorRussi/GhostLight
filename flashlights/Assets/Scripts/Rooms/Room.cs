using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool ghostInRoom;
    public bool playerInRoom;
    public GameObject player;
    public GameObject ghost;

    public void Awake()
    {
        player = GameObject.Find("Player");
        ghost = GameObject.Find("Ghost");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInRoom = true;
            Debug.Log("Player has entered " + gameObject.name);
        }
        else if(collision.gameObject == ghost)
        {
            ghostInRoom = true;
            Debug.Log("Ghost has entered " + gameObject.name);
        }
    }
}
