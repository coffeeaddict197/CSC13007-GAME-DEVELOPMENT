using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Configs/ItemAssets", fileName = "ItemAssets")]
public class ItemAssets : ScriptableObject
{
    public BlockType typeContain;
    public ItemType itemType;
    public string itemName;
    public List<ItemConfigs> listItem;
    public ItemSkillAssets skillAssets;
    public ItemConfigs GetItemConfigByLevel(int level)
    {
        return listItem.Find(x => x.level == level);
    }

    public virtual void ItemHandler()
    {
        
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
            var listMatching = listAssets.FindAll(x => x.typeContain == blockType);
            return listMatching[UnityEngine.Random.Range(0,listMatching.Count)];
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

public enum ItemType
{
    Weapon,
    Helmet,
    Shield,
    Poison
}
