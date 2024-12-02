using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDatas", menuName = "ScriptableObjects/BuildingTypes", order = 1)]
public class BuildingDatas : ScriptableObject
{
    public List<BuildingData> list; 
}
