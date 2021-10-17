using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Configs/ItemHandler/Weapon", fileName = "WeaponHandler")]
public class WeaponHandler : ItemHandler
{
    public float damage;
    public float durability;

    [Header("Position Setup")] public Vector3 initPosition;
    
    public override bool OnEquipItem(BaseBlock block)
    {
        PlayerGear gear = Player.Instance.gears;
        
        if (!gear.IsEquipWeapon)
        {
            Sprite spr = block.blockItem.GetItemSprite();
            gear.EquipWeapon(this,spr);
            return true;
        }
        return false;
    }
}
