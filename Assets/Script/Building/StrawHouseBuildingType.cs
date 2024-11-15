using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrawHouseBuildingType : MonoBehaviour
{
    public BuildingData BuildingData;
    private int WoodGeneratedInOneTime = 1;
    public Vector2Int[] RecordLocations;

    public int GenerateResource()
    {
        return WoodGeneratedInOneTime;
    }

    public Vector2Int[] GetAllLocation(int x, int y)
    {
        RecordLocations = BuildingData.CalculateActualSizeNeeded(x, y);
       
        return RecordLocations;

    }

}
