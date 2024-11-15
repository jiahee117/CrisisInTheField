using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Grid<PathNode> grid;
    public int x;
    public int y;

    public int fCost;
    public int gCost;
    public int hCost;

    public bool walkable;
    public PathNode cameFromPathNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        walkable = true;
    }

    public void CalculateTotalCost()
    {
       fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x+","+y;
    }


}
