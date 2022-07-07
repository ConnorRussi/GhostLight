using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPos;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float fDistanceBetweenNodes;

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

    }
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        
        Vector2 bottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for(int y = 0; y < gridSizeX; y++)
        {
           for(int x = 0; x <gridSizeX; x++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter * nodeRadius);
                bool wall = true;

                if(Physics2D.OverlapPoint(worldPoint, wallMask))
                {
                    wall = false;
                }
               
                
                    grid[x, y] = new Node(wall, worldPoint, x, y);
                
                
            }
        }
    }
    //Function that gets the neighboring nodes of the given node.
    public List<Node> GetNeighboringNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();//Make a new list of all available neighbors.
        int icheckX;//Variable to check if the XPosition is within range of the node array to avoid out of range errors.
        int icheckY;//Variable to check if the YPosition is within range of the node array to avoid out of range errors.

        //Check the right side of the current node.
        icheckX = a_NeighborNode.gridX + 1;
        icheckY = a_NeighborNode.gridY;
        if (icheckX >= 0 && icheckX < gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(grid[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Left side of the current node.
        icheckX = a_NeighborNode.gridX - 1;
        icheckY = a_NeighborNode.gridY;
        if (icheckX >= 0 && icheckX < gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(grid[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Top side of the current node.
        icheckX = a_NeighborNode.gridX;
        icheckY = a_NeighborNode.gridY + 1;
        if (icheckX >= 0 && icheckX < gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(grid[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Bottom side of the current node.
        icheckX = a_NeighborNode.gridX;
        icheckY = a_NeighborNode.gridY - 1;
        if (icheckX >= 0 && icheckX < gridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < gridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(grid[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        return NeighborList;//Return the neighbors list.
    }

    //Gets the closest node to the given world position.
    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float iyPos = ((a_vWorldPos.y + gridWorldSize.y / 2) / gridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((gridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((gridSizeY - 1) * iyPos);

        return grid[ix, iy];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if(grid != null)
        {
            foreach(Node node in grid)
            {
                //if(node.gridY%2 > 0)
                //{
                //    if (node.gridX % 2 > 0)
                //    {
                //        Gizmos.color = Color.blue;
                //    }
                //    else Gizmos.color = Color.red;
                //}
                //else
                //{
                //    if (node.gridX % 2 > 0)
                //    {
                //        Gizmos.color = Color.red;
                //    }
                //    else Gizmos.color = Color.blue;
                //}

                if (node.isWall)
                {
                    Gizmos.color = Color.white;
                }
                else Gizmos.color = Color.yellow;

                if (finalPath != null)
                {
                    if (finalPath.Contains(node))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color of that node
                    }
                }
                if(node == GetComponent<Pathfinding>().TN)
                {
                    Gizmos.color = Color.green;
                }
               
                Gizmos.DrawCube(node.position, Vector2.one * (nodeDiameter - fDistanceBetweenNodes));
            }
            
        }
    }
}
