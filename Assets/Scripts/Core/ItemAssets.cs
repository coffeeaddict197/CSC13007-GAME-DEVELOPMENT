using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemAssets", fileName = "ItemAssets")]
public class ItemAssets : ScriptableObject
{
    public BlockType typeContain;
    public ItemType itemType;
    public string itemName;
    public List<ItemConfigs> listItem;
}
[System.Serializable]
public class ItemAssetConfigs
{
    public List<ItemAssets> listAssets;

    public static ItemAssetConfigs Instance
    {
        get => GameAssetsConfigs.Instance.itemsConfig;
    }

    public ItemConfigs GetAssetConfig(BlockType blockType, string itemName, int level)
    {
        ItemAssets assets = GetAsset(blockType,itemName);
        if (assets != null)
        {
            return assets.listItem.Find(x => x.level == level);
        }
        return null;
    }
    
    public ItemAssets GetAsset(BlockType blockType, string itemName)
    {
        return listAssets.Find(x => x.itemName == itemName && x.typeContain == blockType);
    }
    
}

[System.Serializable]
public class ItemConfigs
{
    public int level;
    public Sprite spr;
}

