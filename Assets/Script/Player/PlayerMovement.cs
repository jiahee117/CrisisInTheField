using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Store behaviour related to Movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    public float moveSpeed;
    Grid<GridObject> buildingGrid;
    public Vector2 vector;

    // Start is called before the first frame update
    void Start()
    {
       
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        buildingGrid = BuildingGrid.Instance.grid;
    }

    void Update()
    {
        GridObject gridObject = buildingGrid.GetValue(this.transform.position);
        if(gridObject!= null)
        {
             vector = gridObject.GetPosition();
            spriteRenderer.sortingOrder = -buildingGrid.GetValue(this.transform.position).GetPosition().y;
        }
    }

    float dis;
    Collision2D collision1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag== "Building")
        {
            Debug.Log("contact");
            

        }
    }
    

    // Update is called once per frame
    public void Move()
    {
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        transform.position += new Vector3(x,y,0)* moveSpeed;
        
        //Flip(x);
    }

    /// <summary>
    /// Flip player
    /// </summary>
    /// <param name="x"></param>
    public void Flip(float x)
    {
        if (x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        
    }
}
