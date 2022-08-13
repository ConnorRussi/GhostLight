using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    //trap variables
    public int numberOfTrapPartsCollected;
    public bool trapReady;
    public bool trapPlaced;
    public bool ghostCollected;
    public GameObject trapPrefab;
    public GameObject trap;
    public Trap trapSC;

    //Trap parts
    public GameObject trapPartPrefab;
    public int numberOfTrapParts;
    public List<GameObject> roomsWithParts;
    public List<GameObject> rooms;
    


    private void Awake()
    {
        player = GameObject.Find("Player");
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            rooms.Add(room);
        }
        SpawnTrapParts();
    }
    public void SpawnTrapParts()
    {
        for(int i = 1; i <= numberOfTrapParts; i++)
        {
            GameObject trapSpawn = rooms[Random.Range(0, rooms.Count - 1)];
            if (trapSpawn.GetComponent<Room>().trapPartInRoom || !trapSpawn.GetComponent<Room>().canHavePart)
            {
                i--;
            }
            else
            {
                trapSpawn.GetComponent<Room>().trapPartInRoom = true;
               GameObject newPart =  Instantiate(trapPartPrefab, trapSpawn.transform.position, Quaternion.identity);
                newPart.transform.position = new Vector3(newPart.transform.position.x, newPart.transform.position.y, 50);
                roomsWithParts.Add(trapSpawn);
            }
           
        }
    }

    public void PlaceTrap()
    {
        trapReady = false;
        trap = Instantiate(trapPrefab, player.transform.position, Quaternion.identity);
        trapSC = trap.GetComponent<Trap>();
        trapPlaced = true;
    }
    
}
