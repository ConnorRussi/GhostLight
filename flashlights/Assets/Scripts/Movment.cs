using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed;
    public float xInput;
    public float yInput;
    public KeyCode trapPlaceKey;
    public KeyCode pickUpKey;
    public bool placeKeyPressed;
    public bool pickUpKeyPressed;
    private GameObject ghost;
    private GhostAI ghostAi;
    private Pathfinding2 pathfinding2;
    private GameManager gameManager;
    public Trap trapSC;
    public GameObject trap;

    public void Awake()
    {
        ghost = GameObject.Find("Ghost");
        ghostAi = ghost.GetComponent<GhostAI>();
        pathfinding2 = ghost.GetComponent<Pathfinding2>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        CollectInputs();
        applyInput();
       
    }
    private void CollectInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
        placeKeyPressed = Input.GetKeyDown(trapPlaceKey);
        pickUpKeyPressed = Input.GetKeyDown(pickUpKey);

    }
    private void applyInput()
    {
        gameObject.transform.Translate(Vector2.left * -xInput * speed * Time.deltaTime, Space.World);
        gameObject.transform.Translate(Vector2.down * -yInput * speed * Time.deltaTime, Space.World);
        if(placeKeyPressed && gameManager.trapReady)
        {
            gameManager.PlaceTrap();
            trap = gameManager.trap;
            trapSC = gameManager.trapSC;
        }
        else if(placeKeyPressed && gameManager.trapPlaced)
        {
            
            trapSC.OpenTrap();
        }
        else if(pickUpKeyPressed && gameManager.trapPlaced && Vector2.Distance(gameObject.transform.position, trap.transform.position) < trapSC.maxPickUpDistance)
        {
                trapSC.pickUpTrap();
            
        }
    }


}
