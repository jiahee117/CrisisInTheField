using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingType : MonoBehaviour
{
    protected BuildingData buildingData;
    
    public Vector2Int[] RecordLocations;
    


    public Vector2Int[] GetAllLocation(int x, int y)
    {
        RecordLocations = buildingData.CalculateActualSizeNeeded(x, y);
       
        return RecordLocations;

    }
}
