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

    public ItemConfigs GetItemConfigByLevel(int level)
    {
        return listItem.Find(x => x.level == level);
    }
}
[System.Serializable]
public class ItemAssetConfigs
{
    public List<ItemAssets> listAssets;

    public static ItemAssetConfigs Instance
    {
        get => GameAssetsConfigs.Instance.itemsConfig;
    }
    
    public ItemAssets GetAsset(BlockType blockType, string itemName="")
    {
        if (itemName.Equals(""))
        {
            return listAssets.Find(x => x.typeContain == blockType);
        }
        return listAssets.Find(x => x.itemName == itemName && x.typeContain == blockType);
    }
    
}

[System.Serializable]
public class ItemConfigs
{
    public int level;
    public Sprite spr;
}

