using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Control behaviour of Player
/// </summary>
public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerState playerState;
    PlayerAction playerAction;
    
    // Start is called before the first frame update
    void Start() 
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();
        playerAction = GetComponent<PlayerAction>();
    }

    void FixedUpdate()
    {
        playerMovement.Move();

    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.Flip(playerAction.moveDirectionPlayer.x);  // Flip player horizontally based on mouseposition


        if (Input.GetMouseButtonDown(0)) 
        {
            playerAction.Fire();
            
        }
    }
}
