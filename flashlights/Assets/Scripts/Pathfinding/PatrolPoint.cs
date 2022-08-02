using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public int pointValue;
    public GameObject ghost;
    public GhostAI ghostAI;
    public LayerMask obstacle;
    public bool inWall;
    public List<GameObject> neighbors;
    public float neighborRadius;
    public GameObject Connection { get; private set; }
    public float g { get; private set; }
    public float h { get; private set; }
    public float f => g + h;

    public void Awake()
    {
        ghost = GameObject.Find("Ghost");
        ghostAI = ghost.GetComponent<GhostAI>();
        var colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.5f, obstacle);
        if (colliders.Length > 0)
        {
            inWall = true;
            Destroy(gameObject);

        }
        var NeighColl = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), neighborRadius);
        foreach(Collider2D collide in NeighColl)
        {
            if(collide.gameObject.layer == gameObject.layer && collide.gameObject != gameObject)
            {
                neighbors.Add(collide.gameObject);
            }

        }
      
        
    }
    public void Start()
    {
        var NeighColl = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), neighborRadius);
        foreach (Collider2D collide in NeighColl)
        {
            if (collide.gameObject.layer == gameObject.layer && collide.gameObject != gameObject)
            {
                neighbors.Add(collide.gameObject);
            }

        }
    }
    public void SetConnection(GameObject patrolPoint) => Connection = patrolPoint;
    
    public void SetG(float G) => g = G;
    public void SetH(float H) => h = H;
    public float GetDistance(GameObject otherObject)
    {
       return Mathf.Abs((otherObject.transform.position.x - gameObject.transform.position.x) + (otherObject.transform.position.y - otherObject.transform.position.y));
    }
    public void changeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }


}
