using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearType
{
    Ring,
    Bracer,
    Amulet,
    Trinker
}
[System.Serializable]
public class GearDatas
{
    public List<GearData> gearDatas;

    public GearData GetDataByType(GearType type)
    {
        return gearDatas.Find(x => x.type == type);
    }
}

[System.Serializable]
public class GearData
{
    public GearType type;
    public int level;
}
