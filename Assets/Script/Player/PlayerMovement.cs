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
    float y;
    float x;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        buildingGrid = BuildingGrid.Instance.grid;
        x=0;y=0;
    }

    void Update()
    {
        GridObject gridValue = buildingGrid.GetValue(transform.position);

        if(gridValue!=null)spriteRenderer.sortingOrder = -(gridValue.GetPosition().y);
        
         x = Input.GetAxis("Horizontal");
         y = Input.GetAxis("Vertical");

        

         dir = new Vector3(x,y).normalized*moveSpeed;
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
         if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
    {
        
        rigidbody2D.velocity = dir;
    }
    else
    {
        rigidbody2D.velocity = Vector3.zero; // Stop immediately when input is released
    }
      //  if (Mathf.Abs(x) < 0.1f) x = 0; // Dead zone for Horizontal
     //   if (Mathf.Abs(y) < 0.1f) y = 0; // Dead zone for Vertical
        
        
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
