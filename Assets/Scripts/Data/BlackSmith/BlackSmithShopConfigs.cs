using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BlackSmith",fileName = "BlackSmithConfigs")]
public class BlackSmithShopConfigs : ScriptableObject
{
    public List<BlackSmithConfig> blackSmithConfigs;

    public static BlackSmithShopConfigs Instance
    {
        get => LoaderUtility.Instance.GetAsset<BlackSmithShopConfigs>("Configs/BlackSmithConfigs");
    }

    public BlackSmithConfig GetConfigs(RewardType type)
    {
        return blackSmithConfigs.Find(x => x.Type == type);
    }
}

[System.Serializable]
public class BlackSmithConfig
{
    public RewardType Type;
    public int DefaultValue;
    public int ValueIncrease;

    public int GetPriceByLevel(int currentLevel)
    {
        return DefaultValue + ValueIncrease * currentLevel;
    }
}
