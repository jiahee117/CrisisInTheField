using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StrawHBuilding", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class StrawHBuilding : BuildingData
{
    
    public StrawHBuilding()
    {
        buildingName = "";

        width = 2 ;

        height =  2 ;

        size = new Vector2Int[4]
        {new Vector2Int(0,0),
         new Vector2Int(1,0),
         new Vector2Int(0,1),
         new Vector2Int(1,1)};

        Health = 0;

        damage = 0;

        upgradelevel = 0 ;

        sprite = null;

        clip = null;
    }
    
}
