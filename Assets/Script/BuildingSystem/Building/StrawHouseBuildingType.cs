using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrawHouseBuildingType : MonoBehaviour
{
    private BuildingData buildingData;
    SpriteRenderer spriteRenderer;
    private int WoodGeneratedInOneTime = 1;
    public Vector2Int[] RecordLocations;

    void Start(){
       spriteRenderer = GetComponentInChildren<SpriteRenderer>();
       spriteRenderer.sprite = buildingData.sprite;

    }

    public int GenerateResource()
    {
        return WoodGeneratedInOneTime;
    }

    public Vector2Int[] GetAllLocation(int x, int y)
    {
        RecordLocations = buildingData.CalculateActualSizeNeeded(x, y);
       
        return RecordLocations;

    }

}
