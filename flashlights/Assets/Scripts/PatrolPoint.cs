using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public int pointValue;
    public GameObject ghost;
    public GhostAI ghostAI;

    public void Start()
    {
        ghost = GameObject.Find("Ghost");
        ghostAI = ghost.GetComponent<GhostAI>();
    }
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject == ghost && ghostAI.target == gameObject)
    //    {
    //       ghostAI.choosePatrolPoint();
    //    }
    //}
}
