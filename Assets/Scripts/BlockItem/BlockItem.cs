using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlockItem : MonoBehaviour
{
    [Header("Configs")] 
    private ItemConfigs _configs;
    [Header("UI")] 
    [SerializeField] private Image itemImg;

    public void InitItem(BlockType blockType,string itemName)
    {
        // var itemAsset = ItemAssetConfigs.Instance.GetAsset(blockType,itemName)
        // _configs = ItemAssetConfigs.Instance.GetAsset(blockType, itemName, 1);
        // if(_configs!=null)
        // {
        //     itemImg.sprite = _configs.spr;
        //     itemType = _configs.
        // }
    }

    public virtual void ItemOnClick()
    {
        
    }
}



public enum ItemType
{
    Weapon,
    Helmet,
    Shield,
    Poison
}

