using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class BuildingGrid : MonoBehaviour
{   
    private Camera mainCam;
    private static BuildingGrid instance;
    public static BuildingGrid Instance
    {
        get{return instance;}
    }
    
    private BuildingDatas bulidngDatas;
    public Transform buildingGhost;
    private Transform buildingGhostModel;
    private SpriteRenderer BuildingGhostspriteRenderer;
    public Grid<GridObject> grid;
    public static event EventHandler<OnPlacedEventArgs> OnPlaced;
    public class OnPlacedEventArgs : EventArgs
    {
        public Vector3 worldPos;
        public int x;
        public int y;

    }

    
    private BuildingData currentSelectedBuilding;

    void Awake()
    {
        instance = this;
        mainCam = Camera.main;
        bulidngDatas = Resources.Load<BuildingDatas>("BuildingType/ScriptObject/"+typeof(BuildingDatas).Name);
        currentSelectedBuilding = bulidngDatas.list[0];

    }

    // Start is called before the first frame update
    void Start()
    {
        
        grid = new Grid<GridObject>(23, 17, 5, this.transform, (Grid<GridObject> g ,int x,int y )=> new GridObject(g,x,y) );
        
        grid.ShowTextArray();
        
        buildingGhostModel = buildingGhost.GetChild(0);
        BuildingGhostspriteRenderer = buildingGhostModel.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        

       
        if (Input.GetMouseButtonDown(1))
        {
            Remove();
            
        }

        if(Input.GetKey(KeyCode.M))
        {
            
            GridObject gridValue = grid.GetValue(mainCam.ScreenToWorldPoint(Input.mousePosition));
            
            if (gridValue == null) return;
            buildingGhost.position = grid.GetWorldPosition(gridValue.GetPosition().x,gridValue.GetPosition().y);
            BuildingGhostspriteRenderer.sprite =  currentSelectedBuilding.sprite;
            
            buildingGhostModel.localScale = currentSelectedBuilding.prefab.GetChild(0).localScale;
            
            //Debug.Log($"Prefab Scale: {currentSelectedBuilding.prefab.GetChild(0).localScale}");
            //Debug.Log($"Building Ghost Model Scale: {buildingGhostModel.localScale}");

            Vector3 scale = Vector3.one;
            buildingGhost.localScale = scale * grid.GetCellSize();

            if(!CheckCanPlace(gridValue))
            {
                BuildingGhostspriteRenderer.color = new UnityEngine.Color(0.8773f,0.5f,0.5f,0.4431373f);
            }else BuildingGhostspriteRenderer.color = new UnityEngine.Color(0.7773f,0.9471698f,0.9468387f,0.4431373f);
            
            buildingGhost.gameObject.SetActive(true);
        
            if (Input.GetMouseButtonDown(0))
            {
                if (gridValue == null) return;
                PlaceObject(gridValue);
            }
            
        }
        


        if(Input.GetKeyDown(KeyCode.K))
        {
            currentSelectedBuilding = bulidngDatas.list[1];

        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            currentSelectedBuilding = bulidngDatas.list[0];

        }

        if(Input.GetKeyDown(KeyCode.M)) 
        {
            for (int x = 0; x < grid.textArray.GetLength(0); x++)
            {
                for (int y = 0; y < grid.textArray.GetLength(1); y++)
                {
                    grid.textArray[x, y].gameObject.SetActive(true);
                    Debug.DrawLine((grid.GetPositionXY(x, y)), (grid.GetPositionXY(x + 1, y)), UnityEngine.Color.red, 1f);
                    Debug.DrawLine(grid.GetPositionXY(x, y), grid.GetPositionXY(x, y + 1), UnityEngine.Color.red, 1f);
                    Debug.DrawLine(grid.GetPositionXY(x + 1, y + 1), grid.GetPositionXY(x, y + 1), UnityEngine.Color.red, 1f);
                    Debug.DrawLine(grid.GetPositionXY(x + 1, y + 1), grid.GetPositionXY(x + 1, y), UnityEngine.Color.red, 1f);


                }
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            buildingGhost.gameObject.SetActive(false);
            for (int x = 0; x < grid.textArray.GetLength(0); x++)
            {
                for (int y = 0; y < grid.textArray.GetLength(1); y++)
                {
                    grid.textArray[x, y].gameObject.SetActive(false);


                }
            }
        }

    }
    
    public void DebugPath(GridObject gridValue)
    {
        List<PathNode> path = Pathfinding.Instance.FindPath(0, 0, gridValue.GetPosition().x, gridValue.GetPosition().y);
        Vector3[] pathPos = Pathfinding.Instance.ConvertToWorldPos(path);
        if (path == null) { Debug.Log("error"); return; }
        if (path != null && pathPos.Length > 1)
        {
            for (int x = 0; x < pathPos.Length - 1; x++)
            {
                Debug.DrawLine(pathPos[x] + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * 0.5f, pathPos[x + 1] + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * 0.5f, UnityEngine.Color.green, 5f); //won't draw if conflict with system.color
            }

        }
    }

    private void Remove()
    {
        GridObject gridValue = grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (gridValue == null) return;

        if (gridValue.GetIsPlaced()) 
        {
            Vector2Int[] locationsincluded = gridValue.GetGameObject().GetComponent<BuildingType>().RecordLocations;

            for (int i = 0; i < locationsincluded.Length; i++)
            {

                GridObject gridObTemp = grid.GetValue(locationsincluded[i].x, locationsincluded[i].y);
                if(gridObTemp.GetGameObject()!=null) Destroy(gridObTemp.GetGameObject());

                gridObTemp.RemoveObject();

               // PathNode pathNode = Pathfinding.Instance.grid.GetValue(grid.GetWorldPosition(gridObTemp.GetPosition().x, gridObTemp.GetPosition().y));
               // pathNode.walkable = true;
            }


            
        }
        //gridValue.RemoveObject();
        
    }

    void PlaceObject(GridObject gridObject)
    {
        if (gridObject != null&&CheckCanPlace(gridObject))
        {
            Transform go = Instantiate(currentSelectedBuilding.prefab, grid.GetWorldPosition(gridObject.GetPosition().x, gridObject.GetPosition().y), Quaternion.identity);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0f);
            Vector3 scale = go.localScale;
            go.localScale = scale * grid.GetCellSize();
            
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(gridObject.GetPosition().y+1);


            BuildingType BuildingType = go.GetComponent<BuildingType>();
            Vector2Int[] locationsincluded= BuildingType.GetAllLocation(gridObject.GetPosition().x,gridObject.GetPosition().y);

            for(int i = 0; i < locationsincluded.Length; i++)
            {
                GridObject gridObTemp = grid.GetValue(locationsincluded[i].x, locationsincluded[i].y);
                gridObTemp.PlaceObject(go.gameObject);
                //PathNode pathNode = Pathfinding.Instance.grid.GetValue(grid.GetWorldPosition(gridObTemp.GetPosition().x, gridObTemp.GetPosition().y));
                //pathNode.walkable = false;
            }


            if (OnPlaced != null) OnPlaced(this, new OnPlacedEventArgs());
        }

    }


    private void CheckWalkable(object sender, Pathfinding.OnPlacedEventArgs e)
    {
        GridObject gridObject = grid.GetValue(e.pathNode.x, e.pathNode.y);
        if (gridObject.GetIsPlaced())
        {
            e.pathNode.walkable = false;
        }
    }

    bool CheckCanPlace(GridObject gridValue)
    {


        int x = gridValue.GetPosition().x;
        int y = gridValue.GetPosition().y;
        Vector2Int[] temp = new Vector2Int[4];
        temp[0].x = 0; temp[0].y = 0;
        temp[1].x = 1; temp[1].y = 0;
        temp[2].x = 0; temp[2].y = 1;
        temp[3].x = 1; temp[3].y = 1;

        for (int k = 0; k < temp.Length; k++)
        {
            temp[k] = temp[k] + new Vector2Int(x, y);

            if (grid.GetValue(temp[k].x, temp[k].y) == null|| grid.GetValue(temp[k].x, temp[k].y).GetIsPlaced()) return false;

        }

        return true;
    }


}
