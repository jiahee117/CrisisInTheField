using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPathFInd : MonoBehaviour
{
    public Transform tf1;
    public Transform tf2;
    private void Start()
    {
        Debug.DrawLine(tf1.position, tf2.position, Color.green, 5f);
    }
}
