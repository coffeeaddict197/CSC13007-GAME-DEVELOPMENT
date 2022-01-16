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

    public void SetupData()
    {
        gearDatas.Add(new GearData{type = GearType.Ring,level = 1});
        gearDatas.Add(new GearData{type = GearType.Amulet,level = 1});
        gearDatas.Add(new GearData{type = GearType.Bracer,level = 1});
        gearDatas.Add(new GearData{type = GearType.Trinker,level = 1});
    }
}

[System.Serializable]
public class GearData
{
    public GearType type;
    public int level;
}
