using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject patrolPointPrefab;
    public Vector2 bottomLeftPossition;
    public int xLength;
    public int yLenght;
    public int numberOfPatrolPoints;
    public int estimatedPatrolPoints;
    public int distanceBetweenPatrolPoints;
    public int estimatedPointsRow;
    public int estimatedPointsCoulum;
    public bool pointsVisible;


    public void Awake()
    {
        bottomLeftPossition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        SpawnGrid();
    }
    public void SpawnGrid()
    {
        estimatedPointsCoulum = (yLenght / distanceBetweenPatrolPoints);
        estimatedPointsRow = (xLength / distanceBetweenPatrolPoints);
        estimatedPatrolPoints = estimatedPointsRow * estimatedPointsCoulum;
        for(int t = 0; t <= estimatedPointsCoulum; t++)
        {
            float ySpawn = bottomLeftPossition.y + distanceBetweenPatrolPoints * t;
            for(int i = 0; i <= estimatedPointsRow; i++)
            {
                Vector2 spawnPoint;
                float xSpawn = bottomLeftPossition.x + distanceBetweenPatrolPoints * i;
                spawnPoint = new Vector2(xSpawn, ySpawn);
                GameObject recentPoint = Instantiate(patrolPointPrefab, new Vector3(xSpawn, ySpawn, 0f), Quaternion.identity);
                recentPoint.name = "Patrol Point(Clone) " + numberOfPatrolPoints;
                if (!pointsVisible)
                {
                    recentPoint.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

                }
                numberOfPatrolPoints++;
            }
            
        }
    }
}
