using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{

    Vector2Int position; 
    private GameObject go;
    bool isPlaced;
    private Grid<GridObject> grid;
    List<GridObject> gridPlaces;
    public GridObject(Grid<GridObject> grid, int x, int y)
    {
        this.grid = grid;
        go = null;
        isPlaced = false;
        this.position.x = x;
        this.position.y = y;
    }

    public GameObject GetGameObject()
    {
        return go;
    }

    public void PlaceObject(GameObject go)
    {
        this.go = go;
        isPlaced=true;
    }

    public void RemoveObject() 
    {
        
        go = null;
        isPlaced=false;

       
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public void SetIsPlaced(bool value)
    {
         isPlaced = value;
    }

    public bool GetIsPlaced()
    {
        return isPlaced;
    }

    public override string ToString()
    {
        return position.ToString();
    }
}
