using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrawHouseBuildingType : BuildingType
{

    SpriteRenderer spriteRenderer;
    private int WoodGeneratedInOneTime = 1;

    void Awake(){
        buildingData = Resources.Load<BuildingData>("BuildingType/StrawHouse");
    }

    void Start(){
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = buildingData.sprite;
    }

    public int GenerateResource()
    {
        return WoodGeneratedInOneTime;
    }

}
