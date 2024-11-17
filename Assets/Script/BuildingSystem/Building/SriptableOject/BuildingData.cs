using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingType", menuName = "ScriptableObjects/BuildingType", order = 0)]
public class BuildingData: ScriptableObject
{
    public string buildingName;

    public int width;
    public int height;
    public Vector2Int[] size;

    public int Health;
    public int damage;
    public int upgradelevel;
    public Sprite sprite;
    public AnimationClip clip;

    public Vector2Int[] CalculateActualSizeNeeded(int x, int y)
    {
        Vector2Int[] temp = new Vector2Int[width* height];
        for(int k = 0; k< temp.Length; k++)
        {
            temp[k] = size[k] + new Vector2Int(x, y);
        }


        return temp;
    }
}
