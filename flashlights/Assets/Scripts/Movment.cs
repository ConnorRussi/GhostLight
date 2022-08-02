using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed;
    public float xInput;
    public float yInput;
    private GameObject ghost;
    private GhostAI ghostAi;
    private Pathfinder pathfinder;
    private Pathfinding2 pathfinding2;

    public void Awake()
    {
        ghost = GameObject.Find("Ghost");
        ghostAi = ghost.GetComponent<GhostAI>();
        pathfinder = ghost.GetComponent<Pathfinder>();
        pathfinding2 = ghost.GetComponent<Pathfinding2>();

    }

    private void Update()
    {
        CollectInputs();
        applyInput();
        if (Input.GetKeyDown(KeyCode.G))
        {
            pathfinding2.Path(ghostAi.FindStartPoint(), ghostAi.FindTargetPoint());
            
            
        }
    }
    private void CollectInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
    private void applyInput()
    {
        gameObject.transform.Translate(Vector2.left * -xInput * speed * Time.deltaTime, Space.World);
        gameObject.transform.Translate(Vector2.down * -yInput * speed * Time.deltaTime, Space.World);
    }


}
