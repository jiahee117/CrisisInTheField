using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyStatus
    {
        Atk,
        Pathfinding,
        Dead,
        Roaming,
    }


    EnemyAction action;
    EnemyState state;
    EnemyMovement enemyMovement;
    EnemyStatus currentStatus;
    Transform playerTF;

    // Start is called before the first frame update
    void Start()
    {
        currentStatus = EnemyStatus.Pathfinding;
        enemyMovement = GetComponent<EnemyMovement>();
        action = GetComponent<EnemyAction>();
        state = GetComponent<EnemyState>();
        playerTF = GameObject.Find("Player").GetComponent<Transform>() ;
        BuildingGrid.OnPlaced += UpdateNodes;
    }

    private void UpdateNodes(object sender, BuildingGrid.OnPlacedEventArgs e)
    {
        enemyMovement.CalculateNodes(transform.position, playerTF.position);
        enemyMovement.x = 0;
    }

    // Update is called once per frame
    void Update()
    {
         CheckStatus();

        switch (currentStatus) 
        {
            case EnemyStatus.Dead: Dead();
                break;
            case EnemyStatus.Atk: Atk();
                break;
            case EnemyStatus.Roaming: Roaming();
                break;
            case EnemyStatus.Pathfinding: PFinding();
                break;

        }


          
        
    }

    private void PFinding()
    {
        if (playerTF != null)
        enemyMovement.FindPath(transform.position, playerTF.position);
    }

    private void Roaming()
    {
        throw new NotImplementedException();
    }

    private void Atk()
    {
        throw new NotImplementedException();
    }

    private void Dead()
    {
        throw new NotImplementedException();
    }

    private void CheckStatus()
    {
        
    }
}
