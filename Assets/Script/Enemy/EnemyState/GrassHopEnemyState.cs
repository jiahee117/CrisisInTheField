using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHopEnemyState : EnemyState
{
    const int MAX_HEALTH_POINT = 100;

    public override void Damaged(int damage)
    {
        healthPoint -= damage;
    }

    public override void Start()
    {
        healthPoint = 100;
    }
}
