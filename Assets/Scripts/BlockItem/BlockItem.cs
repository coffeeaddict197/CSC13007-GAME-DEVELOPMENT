using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlockItem : MonoBehaviour
{
    [Header("Configs")] 
    public ItemAssets config;
    
    [Header("UI")] 
    [SerializeField] private Image itemImg;

    public void InitItem(BlockType blockType,string itemName = "")
    {
        config = ItemAssetConfigs.Instance.GetAsset(blockType);
        if(config!=null)
        {
            itemImg.sprite = config.GetItemConfigByLevel(1).spr;
        }
    }

    public void OnChangeLevel(int level)
    {
        //Ignore first game not init config yet
        if (config != null )
        {
            itemImg.sprite = config.GetItemConfigByLevel(level).spr;
        }
    }
}





