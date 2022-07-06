using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManager : MonoBehaviour
{
 
    public GameObject[] rooms;
    public GameObject[] patrolPointsArray;

    private void Awake()
    {
        //defining the patrol points
        int numberOfPatrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint").Length;
        patrolPointsArray = new GameObject[numberOfPatrolPoints];
        patrolPointsArray = GameObject.FindGameObjectsWithTag("PatrolPoint");

        int numberOfRooms = GameObject.FindGameObjectsWithTag("Room").Length;
        rooms = new GameObject[numberOfRooms];
        rooms = GameObject.FindGameObjectsWithTag("Room");
    }

    
}


