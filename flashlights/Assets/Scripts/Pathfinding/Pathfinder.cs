using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public List<GameObject> FindPath(GameObject startNode, GameObject targetNode)
    {
        var toSearch = new List<GameObject>() { startNode };
        var processed = new List<GameObject>();

        while (toSearch.Count > 0)
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
            {
                if (t.GetComponent<PatrolPoint>().f < current.GetComponent<PatrolPoint>().f)
                {
                    current = t;
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


                foreach (var neighbor in current.GetComponent<PatrolPoint>().neighbors)
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.GetComponent<PatrolPoint>().g + current.GetComponent<PatrolPoint>().GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.GetComponent<PatrolPoint>().g)
                    {
                        neighbor.GetComponent<PatrolPoint>().SetG(costToNeighbor);
                        neighbor.GetComponent<PatrolPoint>().SetConnection(current);
                        if (!inSearch)
                        {
                            neighbor.GetComponent<PatrolPoint>().SetH(current.GetComponent<PatrolPoint>().h);
                            toSearch.Add(neighbor);
                        }
                    }
                }
                return processed;
            }


            

        }
        return null;
    }
}
