using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Configs/ItemHandler/Shield", fileName = "Shield")]
public class ShieldHandler : ItemHandler
{
    public int physicsResistant;
    public override float CurrentDurability
    {
        get => _currentDurability;
        set
        {
            _currentDurability = value;
            ItemEquipSlot.DoFill(ItemType.Shield,_currentDurability/durability,Mathf.Abs(_currentDurability - durability) < 0.1f);
        }
    }
    
    
    public override bool OnEquipItem(BaseBlock block)
    {
        PlayerGear gear = Player.Instance.gears;
        
        if (!gear.IsEquipShield)
        {
            Sprite spr = block.blockItem.GetItemSprite();
            gear.EquipShield(this,spr); 
            ItemEquipSlot.Equip(block.blockItem.config.itemType,block.blockItem.GetItemSprite());
            return true;
        }
        return false;
    }
    
    
    public ShieldHandler Clone()
    {
        return (ShieldHandler)MemberwiseClone();
    }
    
    public static ShieldHandler CreateInstance(ShieldHandler copier)
    {
        var data = ScriptableObject.CreateInstance<ShieldHandler>();
        data = copier.Clone();
        return data;
    }
}
