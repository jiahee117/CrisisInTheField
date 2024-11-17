using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridForBuilding
{
    public Grid<GridObject> grid;

    public GridForBuilding(int width, int height, int cellSize, Transform originTF, Func<Grid<GridObject>,int,int,GridObject> CreateGridObject ){
        grid = new Grid<GridObject>(width,  height, cellSize,  originTF, CreateGridObject );
        grid.ShowTextArray();
    }


}
