using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    
    public int Atk(int healthPoint )
    {
       return healthPoint --;
    }
}
