using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour
{
    private void Start()
    {

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        float fov = 90f;
        Vector3 orgin = Vector3.zero;
        int rayCount = 50;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triagles = new int[rayCount * 3];

        vertices[0] = orgin;

        int vertextIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit = Physics2D.Raycast(orgin, GetVectorFromAngle(angle), viewDistance);
            if (raycastHit.collider == null)
            {
                vertex = orgin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //vertex = orgin + GetVectorFromAngle(angle) * viewDistance;
                vertex = raycastHit.point;
                Debug.Log(raycastHit.point);
            }

            vertices[vertextIndex] = vertex;
            if (i > 0)
            {

                    triagles[triangleIndex + 0] = 0;
                    triagles[triangleIndex + 1] = vertextIndex - 1;
                    triagles[triangleIndex + 2] = vertextIndex;

                    triangleIndex += 3;
             }
                vertextIndex++;
                angle -= angleIncrease;
            



            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 0);
            uv[2] = new Vector2(0, 0);

            triagles[0] = 0;
            triagles[1] = 1;
            triagles[2] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triagles;





            
        }
       
    }
    public Vector3 GetVectorFromAngle(float angleInput)
    {
        float angleRad = angleInput * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
