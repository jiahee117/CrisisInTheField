using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static Grid;

/// <summary>
/// Generic Grid Map
/// </summary>
/// <typeparam name="T"></typeparam>
public class Grid<T>
{
    int height;
    int width;
    public T[,] gridArray;
    public TextMesh[,] textArray;
    int size;
    public Vector3 originPos;
    Transform refTF;


    public event EventHandler<OnGridValueChangedArgs> OnGridValueChanged;

    public class OnGridValueChangedArgs : EventArgs 
    {
       public int x;
       public int y;
    }

    public Grid(int width, int height, int cellSize, Transform originTF, Func<Grid<T>,int,int,T> CreateGridObject )
    {

        this.width = width;
        this.height = height;
        if (originTF == null)
        {
            GameObject gameObject = new GameObject("PathFinder");
            GameObject.Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            refTF = gameObject.GetComponent<Transform>();
        }
        else refTF = originTF;
        size = cellSize;
        gridArray = new T[width, height];
        textArray = new TextMesh[width, height];
        
        originPos = refTF.position;

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = CreateGridObject(this,x,y);

            }
        }

    }

    public void ShowTextArray()
    {
        bool showdebug = true;

        if (showdebug)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    textArray[x, y] = Utilities.CreateWorldText(gridArray[x, y].ToString(), null, GetPositionXY(x, y) + new Vector3(size, size) * 0.5f, 15, TextAnchor.MiddleCenter, Color.black, 0);
                    Debug.DrawLine((GetPositionXY(x, y)), (GetPositionXY(x + 1, y)), Color.red,1f);
                    Debug.DrawLine(GetPositionXY(x, y), GetPositionXY(x, y + 1), Color.red, 1f);
                    Debug.DrawLine(GetPositionXY(x + 1, y + 1), GetPositionXY(x, y + 1), Color.red, 1f);
                    Debug.DrawLine(GetPositionXY(x + 1, y + 1), GetPositionXY(x + 1, y), Color.red,1f);

                }
            }

            OnGridValueChanged += (object sender, OnGridValueChangedArgs eventArgs) =>
            {

                textArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
            };
        }
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public int GetCellSize()
    {
        return size;
    }
    public Vector3 GetPositionXY(int x,int y)
    {
      return new Vector3(x,y)*size + refTF.position ;
    }

    public void SetValue(Vector3 pos, T value)
    {
        int x = Mathf.FloorToInt((pos.x-originPos.x )/ size);
        int y = Mathf.FloorToInt((pos.y - originPos.y)/ size);

        if ((x >= 0 && x < gridArray.GetLength(0)) && (y >= 0 && y < gridArray.GetLength(1)))
        {
            gridArray[x, y] = value;
            textArray[x, y].text = gridArray[x, y].ToString();
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedArgs { x = x, y = y });
        }

        
    }

    public T GetValue(Vector3 pos)
    {
        int x = Mathf.FloorToInt((pos.x - originPos.x) / size);
        int y = Mathf.FloorToInt((pos.y - originPos.y) / size);

        if ((x >= 0 && x < gridArray.GetLength(0)) && (y >= 0 && y < gridArray.GetLength(1)))
        {
            return gridArray[x, y];
     
        }
        return default(T);
    }
    public T GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(T);
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * size + refTF.position;
    }
}
