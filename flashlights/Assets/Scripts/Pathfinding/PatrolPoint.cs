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

    public void Start()
    {
        ghost = GameObject.Find("Ghost");
        ghostAI = ghost.GetComponent<GhostAI>();
      
        var colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.5f, obstacle);
        if (colliders != null && colliders[0].gameObject != gameObject)
        {
            inWall = true;
            gameObject.SetActive(false);
        }
    }
  
    
    
}
