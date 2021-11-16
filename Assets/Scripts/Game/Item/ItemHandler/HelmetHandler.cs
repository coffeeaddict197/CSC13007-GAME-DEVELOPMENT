using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemHandler/Helmet", fileName = "HelmetHandler")]
public class HelmetHandler : ItemHandler
{
    public override float CurrentDurability
    {
        get => _currentDurability;
        set
        {
            _currentDurability = value;
            ItemEquipSlot.DoFill(ItemType.Helmet,_currentDurability/durability,Math.Abs(_currentDurability - durability) < 0.1f);
        }
    }
    public override bool OnEquipItem(BaseBlock block)
    {
        PlayerGear gear = Player.Instance.gears;
        
        if (!gear.IsEquipHelmet)
        {
            Sprite spr = block.blockItem.GetItemSprite();
            gear.EquipHelmet(this,spr);
            ItemEquipSlot.Equip(block.blockItem.config.itemType,block.blockItem.GetItemSprite());
            return true;
        }
        return false;
    }
    
    
    public HelmetHandler Clone()
    {
        return (HelmetHandler)MemberwiseClone();
    }
    
    public static HelmetHandler CreateInstance(HelmetHandler copier)
    {
        var data = ScriptableObject.CreateInstance<HelmetHandler>();
        data = copier.Clone();
        return data;
    }
}
