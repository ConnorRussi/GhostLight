using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour
{
   // public List<GameObject> hitObjects;
    public LayerMask layerMask ;
    private Vector3 orgin;
    float startingAngle;
    public float fov;
    private void LateUpdate()
    {

        DrawFOV();
       
    }
    public void DrawFOV()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
      
        float angle = startingAngle;
        int rayCount = 100;
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
            RaycastHit2D raycastHit = Physics2D.Raycast(orgin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit.collider == null)
            {
                vertex = orgin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = orgin + GetVectorFromAngle(angle) * viewDistance;
                vertex = raycastHit.point;
                Debug.Log(raycastHit.collider.gameObject.name);
               // hitObjects.Add(raycastHit.collider.gameObject);
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
    public float GetAngleFromvectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float  n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void SetOrgin(Vector3 localOrgin)
    {
        orgin = localOrgin;
    }
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromvectorFloat(aimDirection) - fov / 2f;
    }
}
