using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed;
    public float xInput;
    public float yInput;
   


    private void Update()
    {
        CollectInputs();
        applyInput();
    }
    private void CollectInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
    private void applyInput()
    {
        gameObject.transform.Translate(Vector2.left * -xInput * speed * Time.deltaTime, Space.World);
        gameObject.transform.Translate(Vector2.down * -yInput * speed * Time.deltaTime, Space.World);
    }


}
