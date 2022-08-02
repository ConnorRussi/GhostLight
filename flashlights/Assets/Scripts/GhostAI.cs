using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;
    public GameObject target;
    public PatrolPointManager patrolPointManager;
    private Transform playerLastPosition;
    public int RaysForGhostToShoot;
    private bool seePlayer;
    private bool patrolPointSet;
    public int distanceToTeleportToPlayer;
   // public AIDestinationSetter destinationSC;
    public float timeLookingAtPlayer;
    //public AIPath aiPath;
    public float minDistanceToPatrolPoint;
    public LayerMask patrolPoints;



    private void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        patrolPointManager = gameManager.GetComponent<PatrolPointManager>();
       // destinationSC = gameObject.GetComponent<AIDestinationSetter>();
        timeLookingAtPlayer = 0;
        //aiPath = gameObject.GetComponent<AIPath>();
       // target = GameObject.Find("Target");
    }
    //private void Update()
    //{
    //    CheckForPlayer();
    //    chooseAiState();
    //    updateGhostSpeed();
    //}
    //public void CheckForPlayer()
    //{
    //    if (shootRays())
    //    {
    //        seePlayer = true;
    //    }
    //}
    //public void updateGhostSpeed()
    //{
    //    //aiPath.maxSpeed = 5 + (timeLookingAtPlayer / 10);
    //}
    //public bool shootRays()
    //{
    //    return false;
    //}
    //public void chooseAiState()
    //{
    //    if (seePlayer)
    //    {
    //        timeLookingAtPlayer += 1 * Time.deltaTime;
    //        chasePlayer();
    //    }
    //    else
    //    {
    //        if (timeLookingAtPlayer > 0)
    //        {
    //            timeLookingAtPlayer -= 1 * Time.deltaTime;
    //        }
    //        else timeLookingAtPlayer = 0;
    //        Patrol();
    //    }
    //}
    //public void chasePlayer()
    //{
    //    target.transform.position = player.transform.position;
    //    //destinationSC.target = target.transform;
    //}
    //public void Patrol()
    //{
    //    if (!patrolPointSet)
    //    {
    //        target.transform.position = choosePatrolPoint().transform.position;
    //      //  destinationSC.target = target.transform;
    //    }
    //    else if (patrolPointSet)
    //    {
    //        if (minDistanceToPatrolPoint >= Vector2.Distance(gameObject.transform.position, target.transform.position))
    //        {
    //            patrolPointSet = false;
    //        }
    //    }

    //}
    public GameObject choosePatrolPoint()
    {
        patrolPointSet = true;
        return patrolPointManager.patrolPointsArray[Random.Range(0 , patrolPointManager.patrolPointsArray.Length)];
    }
    public GameObject FindTargetPoint()
    {
        var startPoint = Physics2D.OverlapCircleAll(new Vector2(player.transform.position.x, player.transform.position.y), 5, patrolPoints);
        Debug.Log("target point = " + startPoint[0].name);

        return startPoint[0].gameObject;
    }
    public GameObject FindStartPoint()
    {
        var targetPoint = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 5, patrolPoints);
        Debug.Log("Start point = " + targetPoint[0].name);
        return targetPoint[0].gameObject;
    }





}
