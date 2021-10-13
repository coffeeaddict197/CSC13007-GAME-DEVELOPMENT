using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameAssetsConfigs", fileName = "GameAssetsConfigs")]
public class GameAssetsConfigs : ScriptableObject
{
    public static GameAssetsConfigs Instance
    {
        get => LoaderUtility.Instance.GetAsset<GameAssetsConfigs>("Configs/GameAssetsConfigs");
    }
    [Header("Block background configs")]
    public BlockBackgroundConfigs blockBgConfigs;

    [Header("Item configs")] 
    public ItemAssetConfigs itemsConfig;

}



[System.Serializable]
public class BlockBackgroundConfigs
{
    public List<BlockBackgroundConfig> backgroundConfigs;

    public BlockBackgroundConfig GetConfig(BlockType type, int blockLv)
    {
        return backgroundConfigs.Find(x => x.level == blockLv && x.type == type);
    }
}

[System.Serializable]
public class BlockBackgroundConfig
{
    public int level;
    public BlockType type;
    public Sprite spr;
}
