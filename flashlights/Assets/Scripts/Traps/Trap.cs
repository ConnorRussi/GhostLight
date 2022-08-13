using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject ghost;
    public GameObject player;
    public GameManager gameManager;
    public bool ghostTrapped;
    public bool trapOpen;
    public Sprite openTrap;
    public Sprite closedTrap;
    public SpriteRenderer spriteRender;
    public float trapOpenDuration;
    public float maxTrapOpenTime;
    public float maxPickUpDistance;

    //defing variables
    public void Awake()
    {
        ghost = GameObject.Find("Ghost");
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        CloseTrap();
    }
    public void Update()
    {
        trapOpenDuration += (1 * Time.deltaTime);
        if(trapOpen && trapOpenDuration > maxTrapOpenTime)
        {
            CloseTrap();
        }
    }
    //checks if everything is set up for ghost to be trapped
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == ghost && trapOpen)
        {
            TrapGhost();
        }
        else
        {
            return;
        }
    }
    //Traps the ghost
   public void TrapGhost()
    {
        ghost.SetActive(false);
        ghostTrapped = true;
        spriteRender.sprite = closedTrap;
    }

    public void pickUpTrap()
    {
        gameManager.trapReady = true;
        gameManager.trapPlaced = false;
        if (ghostTrapped)
        {
            gameManager.ghostCollected = true;
            
        }
        Destroy(gameObject);
    }
    public void OpenTrap()
    {
        if (!trapOpen)
        {
            trapOpenDuration = 0;
            trapOpen = true;
        }
        return;
    }

    public void CloseTrap()
    {
        if (trapOpen)
        {
            trapOpen = false;
        }
        return;
    }
}
