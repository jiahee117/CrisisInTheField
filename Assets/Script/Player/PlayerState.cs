using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store Player State
/// </summary>
public class PlayerState : MonoBehaviour
{
    int currentHealthPoint;
    int fullHealthPoint;
    private void Start()
    {
        currentHealthPoint = 100;
        fullHealthPoint = 100;
    }

    public void Damaged(int atk)
    {
        currentHealthPoint -= atk;
    }
}
