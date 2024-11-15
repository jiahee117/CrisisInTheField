using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store behaviour related to Movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        controller.Move(new Vector2(x * Time.deltaTime * moveSpeed, y * Time.deltaTime * moveSpeed));
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
