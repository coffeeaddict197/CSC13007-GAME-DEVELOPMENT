using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemHandler/Helmet", fileName = "HelmetHandler")]
public class HelmetHandler : ItemHandler
{
    public override bool OnEquipItem(BaseBlock block)
    {
        PlayerGear gear = Player.Instance.gears;
        
        if (!gear.IsEquipHelmet)
        {
            Sprite spr = block.blockItem.GetItemSprite();
            gear.EquipHelmet(this,spr);
            return true;
        }
        return false;
    }
}
