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
    Rigidbody2D rigidbody2D;
    public float moveSpeed;
    Grid<GridObject> buildingGrid;
    public Vector2 vector;
    public Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        buildingGrid = BuildingGrid.Instance.grid;
    }

    void Update()
    {
        
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
        GridObject gridValue = buildingGrid.GetValue(transform.position);

        if(gridValue!=null)spriteRenderer.sortingOrder = -(gridValue.GetPosition().y);
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        

      //  if (Mathf.Abs(x) < 0.1f) x = 0; // Dead zone for Horizontal
     //   if (Mathf.Abs(y) < 0.1f) y = 0; // Dead zone for Vertical



        dir = transform.position + new Vector3(x,y).normalized*moveSpeed;
        rigidbody2D.MovePosition(dir);
        
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
