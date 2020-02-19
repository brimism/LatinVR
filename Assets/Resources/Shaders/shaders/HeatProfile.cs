using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HeatProfile", menuName = "ScriptableObjects/HeatmapScriptableObject", order = 1)]
public class HeatProfile : ScriptableObject
{
    public uint studentID;
    public string studentName;
    public string date;
}
