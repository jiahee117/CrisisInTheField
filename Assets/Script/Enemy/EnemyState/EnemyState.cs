using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    
    protected int healthPoint;

    public abstract void Start();
    public abstract void Damaged(int damage);



}
