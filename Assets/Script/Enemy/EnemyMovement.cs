using GridPathfindingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyMovement : MonoBehaviour
{
    public static event EventHandler<OnGridEventArgs> OnGrid;
    public class OnGridEventArgs : EventArgs
    {
        public PathNode pathNode;

    }

    List<PathNode> paths;

    [SerializeField]
    Vector3[] nodes;
    Vector3[] nextPath;

    Vector3 lastNode;
    public float moveSpeed;
    public int x = 0;
    float time = 1;

    

    public void FindPath(Vector3 startPos, Vector3 endPos)
    {
        //if (OnGrid != null) OnGrid(this, new OnGridEventArgs() { pathNode = Pathfinding.Instance.grid.GetValue(startPos) } );
        time -= Time.deltaTime;
        
        if (time <= 0 )
        {
            CalculateNodes(startPos, endPos);
            time = 0.75f;
        }
        
        if (nodes == null) return;

        if (x < nodes.Length && (nodes[x] - transform.position).magnitude > (0.1f) && paths[x].walkable)
        {
            MoveTo(nodes[x]);
            
        }
        else
        {
            x++;      
        }
        if (x >= nodes.Length) {x = 0; }
        
    }

    public Vector3[] CalculateNodes(Vector3 startPos, Vector3 endPos)
    {
        x= 1;
        Vector2Int start, end;
        bool isSuccess = ConvertWorldPosToLocationIndexs(startPos, endPos, out start, out end);

       
        
        if (isSuccess && Pathfinding.Instance.grid.gridArray[start.x, start.y].walkable)
        {
            paths = Pathfinding.Instance.FindPath(start.x, start.y, end.x, end.y);
            nodes = Pathfinding.Instance.ConvertToWorldPos(paths);
            if (nodes != null) { for (int x = 0; x < nodes.Length; x++) { Vector3 temp = nodes[x] + new Vector3(1, 1, 0) * Pathfinding.Instance.grid.GetCellSize() * 0.5f; nodes[x] = temp; } }
        }
        return nodes;
    }

    private void Kostan(int x, int y)
    {
        Quaternion qr =transform.rotation;
        Vector3 euler = qr.eulerAngles;

        Vector3 targetPos = Pathfinding.Instance.grid.GetWorldPosition(x, y) + new Vector3(1, 1, 0) * Pathfinding.Instance.grid.GetCellSize() * 0.5f;

        Vector3 dir = (transform.position - targetPos).normalized;
        Vector3 vector = Vector3.zero;

        

        float cosX = Vector3.Dot(dir, Vector3.up);

        if (cosX < 0.707 && cosX > -0.707)  //if -0.707 <cosX< 0.707
        {
            if (dir.x > 0)
            {
                dir = Vector3.right;
            }
            else dir = Vector3.left;
        }else
        {
            if (dir.y > 0)
            {
                dir = Vector3.up;
            }
            else dir = Vector3.down;
        }
        
        transform.position+=dir * Time.deltaTime * moveSpeed;
    }

    private bool ConvertWorldPosToLocationIndexs(Vector3 startPos, Vector3 endPos, out Vector2Int start, out Vector2Int end)
    {
        start = new Vector2Int();
        end = new Vector2Int();

        PathNode[] pathNodes = new PathNode[]
        {
        Pathfinding.Instance.grid.GetValue(startPos),
        Pathfinding.Instance.grid.GetValue(endPos)
        };

        if (pathNodes.Contains(null)) return false;

        start.x = pathNodes[0].x;
            start.y = pathNodes[0].y;
            end.x = pathNodes[1].x;
            end.y = pathNodes[1].y;


        return true;
    }

    private bool ConvertWorldPosToLocationIndex(Vector3 targetPos, out Vector2Int target)
    {
        target = new Vector2Int();

        PathNode pathNode = Pathfinding.Instance.grid.GetValue(targetPos);

        if (pathNode == null) return false;

        target.x = pathNode.x;
        target.y = pathNode.y;


        return true;
    }

    void MoveTo(Vector3 target)
    {

            transform.position += (target - transform.position).normalized * Time.deltaTime * moveSpeed; 
    }
}
