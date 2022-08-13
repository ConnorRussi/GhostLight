using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPart : MonoBehaviour
{
    public bool partCollected;
    public GameManager gameManager;
    public GameObject player;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            gameManager.numberOfTrapPartsCollected++;
            partCollected = true;
            if(gameManager.numberOfTrapPartsCollected == gameManager.numberOfTrapParts)
            {
                gameManager.trapReady = true;
            }
            gameObject.SetActive(false);
        }
    }
}
