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
    public bool seePlayer;
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

    //Checking for player in view
    public float rangeOfView;
    public LayerMask obstacles;
    public LayerMask playerLayer;
    public GameObject hit;


    //Facing the ghost to player;
    public float rotationModifier;
    public float rotationSpeed;

    //Choosing a random room
    public List<GameObject> rooms;
    public GameObject currentRoom;
    public bool roomSet;
    public float minDistanceToRoom;

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
       foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            rooms.Add(room);
        }
        target = FindStartPoint();
    }
    private void Update()
    {
        CheckForPlayer();
        chooseAiState();
        MoveGhost();
       // RotateGhost();
    }
    public void RotateGhost()
    {
        if (player != null)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
    }
    public void CheckForPlayer()
    {
        if (shootRays())
        {
            seePlayer = true;
            // aiPath.maxSpeed = 5 + (timeLookingAtPlayer / 10);
        }
        else seePlayer = false;
    }
    
    //Checks for player
    public bool shootRays()
    {
        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, player.transform.position, obstacles) ;
        if (hit.collider == null || hit.collider.gameObject == null) 
        {
            return true;
            //RaycastHit2D toPlayer = Physics2D.Linecast(gameObject.transform.position, player.transform.position);
            //if(toPlayer.rigidbody != null && toPlayer.collider.gameObject != null)
            //{
            //    if (toPlayer.collider.gameObject.CompareTag("Player"))
            //    {
                   
            //    }
            //}
          
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, player.transform.position, obstacles);
        //Gizmos.DrawLine(transform.position, player.transform.position);
        Gizmos.DrawLine(gameObject.transform.position, hit.collider.gameObject.transform.position);

        Gizmos.color = Color.blue;

        RaycastHit2D toPlayer = Physics2D.Linecast(gameObject.transform.position, player.transform.position, playerLayer);
        Gizmos.DrawLine(gameObject.transform.position, toPlayer.collider.gameObject.transform.position);
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
        target = player;
       
    }
    public void Patrol()
    {
        if (!roomSet)
        {
            currentRoom = chooseRoomToWanderTo();
            roomSet = true;
        }
        else if(roomSet)
        {
            if (minDistanceToRoom >= Vector2.Distance(gameObject.transform.position, currentRoom.transform.position))
            {
                roomSet = false;
            }
            else
            {
                if (!patrolPointSet || target == player)
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
            
        }

    }
    public GameObject choosePatrolPoint()
    {
        patrolPointSet = true;
        path = pathfinding.Path(FindStartPoint(), FindTargetPoint(currentRoom));
        GameObject newPoint = path[path.Count - 1];
        if(newPoint == lastPatrolPoint || lastPatrolPoint == null)
        {
            newPoint = path[path.Count - 2];
            lastPatrolPoint = newPoint;
        }
        else lastPatrolPoint = newPoint;

        return lastPatrolPoint;
    }
    public GameObject FindTargetPoint(GameObject target)
    {
        var startPoints = Physics2D.OverlapCircleAll(new Vector2(target.transform.position.x, target.transform.position.y), 5, patrolPoints);
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
        var targetPoints = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 1, patrolPoints);
        GameObject targetPoint = targetPoints[0].gameObject;
        foreach(Collider2D point in targetPoints)
        {
            if(point.gameObject.layer == patrolPoints && targetPoint.gameObject.GetComponent<PatrolPoint>().GetDistance(player) > point.gameObject.GetComponent<PatrolPoint>().GetDistance(player))
            {
                targetPoint = point.gameObject;
                Debug.Log(targetPoint.name);
            }
        }
        Debug.Log("Start point = " + targetPoint.name);
        return targetPoint.gameObject;
    }
    public void MoveGhost()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, target.transform.position, ghostSpeed * Time.deltaTime) ;
    }

   public int DistanceToPlayer()
    {
        
            return Mathf.RoundToInt(Mathf.Abs((player.transform.position.x - gameObject.transform.position.x) + (player.transform.position.y - gameObject.transform.position.y)));
        
    }
    public GameObject chooseRoomToWanderTo()
    {
        return rooms[Random.Range(0, rooms.Count - 1)];
       
       
    }
}
