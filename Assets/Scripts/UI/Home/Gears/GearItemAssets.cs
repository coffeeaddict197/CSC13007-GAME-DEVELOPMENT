using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GearItemData",fileName = "GearData")]
public class GearItemAssets : ScriptableObject
{
    public List<GearConfig> gearConfigs;

    public static GearItemAssets Instance
    {
        get => LoaderUtility.Instance.GetAsset<GearItemAssets>("Configs/GearAssetConfigs");
    }

    public GearConfig GetAsset(GearType type)
    {
        return gearConfigs.Find(x => x.type == type);
    }
}

[System.Serializable]
public class GearConfig
{
    public string name;
    public GearType type;
    public Sprite imgRrepresent;
    public GearStats stats;
    public int valueAfterLevelup;
    
}

[System.Serializable]
public class GearStats
{
    public GearStatEnum statsEnum;
    public int defaultValue;
    public Sprite statsImg;

}

public enum GearStatEnum
{
    Health,
    Attack,
    Defense,
    Luck
}