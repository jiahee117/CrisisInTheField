using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BuildingType : MonoBehaviour
{
    public BuildingData buildingData;
    private SpriteRenderer spriteRenderer;
    
    public Vector2Int[] RecordLocations;

    protected virtual void Awake(){
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null) {
        Debug.LogError("SpriteRenderer is missing!");
        return;
    }
    if (buildingData == null || buildingData.sprite == null) {
        Debug.LogError("BuildingData or its sprite is not assigned!");
        return;
    }
        spriteRenderer.sprite = buildingData.sprite;
    }
    


    public Vector2Int[] GetAllLocation(int x, int y)
    {
        RecordLocations = buildingData.CalculateActualSizeNeeded(x, y);
       
        return RecordLocations;

    }
}
