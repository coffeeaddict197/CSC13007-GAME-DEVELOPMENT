using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Configs/ItemHandler/Shield", fileName = "Shield")]
public class ShieldHandler : ItemHandler
{
    public override bool OnEquipItem(BaseBlock block)
    {
        PlayerGear gear = Player.Instance.gears;
        
        if (!gear.IsEquipShield)
        {
            Sprite spr = block.blockItem.GetItemSprite();
            gear.EquipShield(this,spr);
            return true;
        }

        return false;
    }
}
