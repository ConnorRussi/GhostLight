using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    public GameObject target;
    private GameObject lastPatrolPoint;
    public PatrolPointManager patrolPointManager;
    private Transform playerLastPosition;
    public int RaysForGhostToShoot;
    private bool seePlayer;
    public bool patrolPointSet;
    public bool pathSet;
    public int distanceToTeleportToPlayer;
   // public AIDestinationSetter destinationSC;
    public float timeLookingAtPlayer;
    //public AIPath aiPath;
    public float minDistanceToPatrolPoint;
    public LayerMask patrolPoints;
    private Pathfinding2 pathfinding;
    public float ghostSpeed;
    public List<GameObject> path;


    private void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        patrolPointManager = gameManager.GetComponent<PatrolPointManager>();
        pathfinding = gameObject.GetComponent<Pathfinding2>();
       // destinationSC = gameObject.GetComponent<AIDestinationSetter>();
        timeLookingAtPlayer = 0;
        //aiPath = gameObject.GetComponent<AIPath>();
       // target = GameObject.Find("Target");
    }
    private void Update()
    {
        CheckForPlayer();
        chooseAiState();
        MoveGhost();
    }
    public void CheckForPlayer()
    {
        if (shootRays())
        {
            seePlayer = true;
           // aiPath.maxSpeed = 5 + (timeLookingAtPlayer / 10);
        }
    }
    
    //Checks for player
    public bool shootRays()
    {
        return false;
    }
    public void chooseAiState()
    {
        if (seePlayer)
        {
            timeLookingAtPlayer += 1 * Time.deltaTime;
            chasePlayer();
        }
        else
        {
            if (timeLookingAtPlayer > 0)
            {
                timeLookingAtPlayer -= 1 * Time.deltaTime;
            }
            else timeLookingAtPlayer = 0;
            Patrol();
        }
    }
    public void chasePlayer()
    {
        target.transform.position = player.transform.position;
        //destinationSC.target = target.transform;
    }
    public void Patrol()
    {
        if (!patrolPointSet)
        {
            target = choosePatrolPoint();
           
        }
        else if (patrolPointSet)
        {
            if (minDistanceToPatrolPoint >= Vector2.Distance(gameObject.transform.position, target.transform.position))
            {
                patrolPointSet = false;
            }
        }

    }
    public GameObject choosePatrolPoint()
    {
        patrolPointSet = true;
        path = pathfinding.Path(FindStartPoint(), FindTargetPoint());
        GameObject newPoint = path[path.Count - 1];
        if(newPoint == lastPatrolPoint || lastPatrolPoint == null)
        {
            newPoint = path[path.Count - 2];
            lastPatrolPoint = newPoint;
        }
        else lastPatrolPoint = newPoint;

        return lastPatrolPoint;
    }
    public GameObject FindTargetPoint()
    {
        var startPoints = Physics2D.OverlapCircleAll(new Vector2(player.transform.position.x, player.transform.position.y), 5, patrolPoints);
        GameObject targetPoint = startPoints[0].gameObject;
        foreach (Collider2D point in startPoints)
        {
            if (point.gameObject.layer == patrolPoints && targetPoint.gameObject.GetComponent<PatrolPoint>().GetDistance(player) > point.gameObject.GetComponent<PatrolPoint>().GetDistance(player))
            {
                targetPoint = point.gameObject;
            }
        }
        Debug.Log("target point = " + targetPoint.name);

        return targetPoint.gameObject;
    }
    public GameObject FindStartPoint()
    {
        var targetPoints = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 5, patrolPoints);
        GameObject targetPoint = targetPoints[0].gameObject;
        foreach(Collider2D point in targetPoints)
        {
            if(point.gameObject.layer == patrolPoints && targetPoint.gameObject.GetComponent<PatrolPoint>().GetDistance(player) > point.gameObject.GetComponent<PatrolPoint>().GetDistance(player))
            {
                targetPoint = point.gameObject;
            }
        }
        Debug.Log("Start point = " + targetPoint.name);
        return targetPoint.gameObject;
    }
    public void MoveGhost()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, target.transform.position, ghostSpeed * Time.deltaTime) ;
    }





}
