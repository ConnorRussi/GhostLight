using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool ghostInRoom;
    public bool playerInRoom;
    public bool trapPartInRoom;
    public bool canHavePart;
    public GameObject player;
    public GameObject ghost;
    public List<GameObject> points;

    public void Awake()
    {
        player = GameObject.Find("Player");
        ghost = GameObject.Find("Ghost");
    }

    public void Start()
    {
       Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x, transform.position.y), gameObject.GetComponent<BoxCollider2D>().size, 1.0f);
        foreach(Collider2D possiblePoints in hits)
        {
            if(possiblePoints.gameObject.tag == "PatrolPoint")
            {
                points.Add(possiblePoints.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInRoom = true;
            //Debug.Log("Player has entered " + gameObject.name);
        }
        else if(collision.gameObject == ghost)
        {
            ghostInRoom = true;
            //Debug.Log("Ghost has entered " + gameObject.name);
        }
        //else Debug.Log(collision.gameObject.name);
    }
}
