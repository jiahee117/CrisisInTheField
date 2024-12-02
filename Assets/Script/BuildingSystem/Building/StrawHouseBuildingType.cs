using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrawHouseBuildingType : BuildingType
{

    private int WoodGeneratedInOneTime = 1;

    protected override void Awake(){
        base.Awake();

    }

    void Start(){
       
    }

    public int GenerateResource()
    {
        return WoodGeneratedInOneTime;
    }

}
