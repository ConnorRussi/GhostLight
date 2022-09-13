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
    public bool isWalking;
    private GameObject ghost;
    private GhostAI ghostAi;
    private Pathfinding2 pathfinding2;
    private GameManager gameManager;
    public Trap trapSC;
    public GameObject trap;

    //Audio
    public AudioSource audioSource;
    public AudioClip[] footSteps;
    public AudioClip currentFootStep;
    public void Awake()
    {
        ghost = GameObject.Find("Ghost");
        ghostAi = ghost.GetComponent<GhostAI>();
        pathfinding2 = ghost.GetComponent<Pathfinding2>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        currentFootStep = footSteps[0];
    }

    private void Update()
    {
        CollectInputs();
        applyInput();
        if(isWalking) FootSteps();


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
        if (xInput != 0 || yInput != 0)
        {
            isWalking = true;
        }
        else isWalking = false;
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
    private void FootSteps()
    {
        if (!audioSource.isPlaying)
        {
            currentFootStep = ChooseFootStep();
            audioSource.PlayOneShot(currentFootStep);
        }
    }
    private AudioClip ChooseFootStep()
    {
        return footSteps[Random.Range(0, footSteps.Length)];
    }
}
