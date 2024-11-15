using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class testing : MonoBehaviour
{
    public Grid<bool> grid;
    HeatMapVisual<bool> heatMapVisual;
    
    // Start is called before the first frame update
    void Start()
    {
        new Pathfinding(5, 5, 5, this.transform);
        grid = new Grid<bool>(5, 5, 40, transform,(Grid<bool> g,int x,int y)=> new bool());

        heatMapVisual = new HeatMapVisual<bool>(grid, GetComponent<MeshFilter>());

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bool gridValue = grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), !gridValue);
            

        }
    }
    void LateUpdate()
    {
        if (heatMapVisual.gridValueChanged)
        {
            heatMapVisual.gridValueChanged = false;

            heatMapVisual.UpdateHeatMap();
        }
    }




}
