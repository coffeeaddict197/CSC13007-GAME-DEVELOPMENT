using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
    public bool isEquiped;
    
    [Header("Weapon")] 
    [SerializeField] SpriteRenderer sprWeapon;
    private WeaponHandler _weapon;

    [Header("Helmet")] 
    [SerializeField] private Animator anim;
    [SerializeField] SpriteRenderer sprHelmet;
    private HelmetHandler _helmet;
    
    [Header("Shield")]
    [SerializeField] SpriteRenderer sprShield;
    private ShieldHandler _shield;

    
    //Weapon
    public void EquipWeapon(WeaponHandler weapon, Sprite weaponSpr)
    {
        _weapon = WeaponHandler.CreateInstance(weapon);
        _weapon.Init();
        sprWeapon.sprite = weaponSpr;
        sprWeapon.transform.localPosition = weapon.initPosition;

        isEquiped = true;
    }
    public bool IsEquipWeapon => _weapon != null;

    public void DropWeapon()
    {
        _weapon = null;
        sprWeapon.sprite = null;
        ItemEquipSlot.UnEquip(ItemType.Weapon);
    }

    public int TakeDamage()
    {
        if (_weapon != null)
        {
            _weapon.CurrentDurability -= 10;
            if(_weapon.CurrentDurability <= 0)
            {
                DropWeapon();
            }
            return Ultility.RandomIn(_weapon.damage,_weapon.damage-10,_weapon.damage+10);
        }

        return WeaponHandler.GetDefaulDamage();
    }
    
    
    //Helmet
    public void EquipHelmet(HelmetHandler helmet, Sprite helmetSpr)
    {
        anim.SetLayerWeight(1,1f);
        _helmet = HelmetHandler.CreateInstance(helmet);
        _helmet.Init();
        sprHelmet.sprite = helmetSpr;

        isEquiped = true;
    }

    public int HelmetPhysicsResistant()
    {
        if (_helmet != null)
            return _helmet.physicsResistant;
        return 0;
    }

    public void AffectHelmetDurability(float durability)
    {
        if (_helmet == null)
            return;
        _helmet.CurrentDurability -= durability;
        if(_helmet.CurrentDurability <= 0)
            DropHelmet();
    }
    
    
    public bool IsEquipHelmet => _helmet != null;

    public void DropHelmet()
    {
        anim.SetLayerWeight(1,0f);
        _helmet = null;
        sprHelmet.sprite = null;
        ItemEquipSlot.UnEquip(ItemType.Helmet);
    }
    
    
    //Shield
    public void EquipShield(ShieldHandler shield, Sprite shieldSpr)
    {
        _shield = ShieldHandler.CreateInstance(shield);
        _shield.Init();
        sprShield.sprite = shieldSpr;
        
        isEquiped = true;

    }
    public bool IsEquipShield => _shield != null;

    public void DropShield()
    {
        _shield = null;
        sprShield.sprite = null;
        ItemEquipSlot.UnEquip(ItemType.Shield);
    }

    public void AffectShieldDurability(float durability)
    {
        if (_shield == null)
            return;
        _shield.CurrentDurability -= durability;
        if (_shield.CurrentDurability <= 0)
        {
            DropShield();
        }
    }
    
    public int SheildPhysicsResistant()
    {
        if (_shield != null)
            return _shield.physicsResistant;
        return 0;
    }

    public void UnEquipAllGear()
    {
        DropShield();
        DropWeapon();
    }

}
