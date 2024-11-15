using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Pathfinding
{
    public event EventHandler<OnPlacedEventArgs> OnPlaced;

    public class OnPlacedEventArgs : EventArgs
    {
        public PathNode pathNode;
        
    }

    public static Pathfinding Instance;
    const int MOVE_DIAGONAL_COST = 14;
    const int MOVE_STRAIGHT_COST = 10;

    public Grid<PathNode> grid;
    private List<PathNode> openList;
    public List<PathNode> closeList;
    List<PathNode> NeighbourNodeList;
    List<PathNode> path;
    public Pathfinding(int width, int height,int size,Transform transform)
    {
        grid = new Grid<PathNode>(width, height, size, transform, (Grid<PathNode> g,int x,int y) => new PathNode(g, x, y));
        openList = new List<PathNode>();
        closeList = new List<PathNode>();
        NeighbourNodeList = new List<PathNode>();
        path = new List<PathNode>();
        Instance = this;
        //grid.ShowTextArray();

    }
 
    public void SetGrid(Grid grid)
    {
    }

    public Vector3[] ConvertToWorldPos(List<PathNode> path)
    {
        if (path == null) return null;
        Vector3[] pathWorldPos = new Vector3[path.Count]; 

       
        for (int x =0; x < pathWorldPos.Length;x++)
        {
            int i = path[x].x;
            int j = path[x].y;
            pathWorldPos[x] = grid.GetWorldPosition(i,j);
        }
        return pathWorldPos;
    }

    public List<PathNode> FindPath(int startX,int startY,int endX, int endY)
    {
        PathNode startNode = grid.GetValue(startX, startY);
        PathNode endNode = grid.GetValue(endX, endY);
        openList.Clear();
        closeList.Clear();
        openList.Add(startNode);

        


        // reset node
        for (int x = 0; x< grid.GetWidth(); x++)
        {
            for(int y = 0;y< grid.GetHeight();y++)
            {
                PathNode pathNode = grid.GetValue(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateTotalCost();
                pathNode.cameFromPathNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateTotalCost();

        //cycle

        while (openList.Count > 0) 
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            

            if (!currentNode.walkable) 
            {
                openList.Remove(currentNode);
                closeList.Add(currentNode);
                continue;
            }

            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach(PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closeList.Contains(neighbourNode)) continue;

                int tentativeCost = currentNode.gCost + CalculateDistanceCost (currentNode, neighbourNode);
                if(tentativeCost< neighbourNode.gCost)
                {
                    neighbourNode.gCost = tentativeCost;
                    neighbourNode.cameFromPathNode= currentNode;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateTotalCost();
                }
                if(!openList.Contains(neighbourNode)) openList.Add(neighbourNode);

            }

        }
        return null;

    } 

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        path.Clear();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.cameFromPathNode != null)
        {
            path.Add(currentNode.cameFromPathNode);
            currentNode = currentNode.cameFromPathNode;
        }
        path.Reverse();
        return path;
    }
    
    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        NeighbourNodeList.Clear();

        if (currentNode.x -1 >= 0) //left
        {
            
            NeighbourNodeList.Add(GetNode(currentNode.x-1,currentNode.y));

            if (currentNode.y- 1 >= 0) //left down
            {
                NeighbourNodeList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            }
            if (currentNode.y + 1 < grid.GetHeight()) //left up
            {
                NeighbourNodeList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }
        }
        if (currentNode.x + 1 < grid.GetWidth()) //right
        {
            NeighbourNodeList.Add(GetNode(currentNode.x + 1, currentNode.y));

            if (currentNode.y - 1 >= 0) //right down
            {
                NeighbourNodeList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            }
            if (currentNode.y + 1 < grid.GetHeight()) //right up
            {
                NeighbourNodeList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }
        }

        if (currentNode.y - 1 >= 0) // down
        {
            NeighbourNodeList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }
        if (currentNode.y + 1 < grid.GetHeight()) // up
        {
            NeighbourNodeList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }
        return NeighbourNodeList;

    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetValue(x, y);
    }

    private int CalculateDistanceCost(PathNode a, PathNode b) //IF CANT FIND BEST PATH SOMETHING WENT WRONG HERE
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST*Mathf.Min(xDistance, yDistance)+MOVE_STRAIGHT_COST*remaining ;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
    {


        PathNode lowestPathNode = pathNodes[0];

        for(int x = 0 ;x< pathNodes.Count;x++) 
        {
            if (pathNodes[x].fCost < lowestPathNode.fCost)
            {
                lowestPathNode = pathNodes[x];
            }
        }


        return lowestPathNode;
    }
}
