using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding2 : MonoBehaviour
{
    public void Path(GameObject startNode, GameObject targetNode)
    {
        var toSearch = new List<GameObject>() { startNode };
        var processed = new List<GameObject>();

        while (toSearch.Count > 0)
        {
            var current = toSearch[0];
           // current.GetComponent<PatrolPoint>().SetH(current.GetComponent<PatrolPoint>().GetDistance(targetNode));

            foreach (var h in toSearch)
            {
                var t = h.GetComponent<PatrolPoint>();
                
                if (t.f < current.GetComponent<PatrolPoint>().f || t.f == current.GetComponent<PatrolPoint>().f && t.h < current.GetComponent<PatrolPoint>().h)
                {
                    current = h;
                    

                }
            }
            processed.Add(current);
            toSearch.Remove(current);
            if (current == targetNode)
            {
                var currentPathTile = targetNode;
                var path = new List<GameObject>();
                var count = 100;
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.GetComponent<PatrolPoint>().Connection;
                    count--;
                    Debug.Log("sdfsdf");
                }
                for (int i = 0; i < path.Count; i++)
                {
                    path[i].GetComponent<PatrolPoint>().changeColor();

                    Debug.Log(path[i].name);
                }
            }
            foreach (GameObject neighbor in current.GetComponent<PatrolPoint>().neighbors)
            {
                if (!processed.Contains(neighbor))
                {
                    bool inSearch = toSearch.Contains(neighbor);
                    bool inFinalPath = processed.Contains(neighbor);
                    float costToNeighbor = current.GetComponent<PatrolPoint>().g + current.GetComponent<PatrolPoint>().GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.GetComponent<PatrolPoint>().g)
                    {
                        neighbor.GetComponent<PatrolPoint>().SetG(costToNeighbor);
                        neighbor.GetComponent<PatrolPoint>().SetConnection(current);
                        if (!inSearch && !inFinalPath)
                        {
                            neighbor.GetComponent<PatrolPoint>().SetH(neighbor.GetComponent<PatrolPoint>().GetDistance(targetNode));
                            toSearch.Add(neighbor);
                        }
                    }
                }
               
            }
            
        }


    }
        
}



