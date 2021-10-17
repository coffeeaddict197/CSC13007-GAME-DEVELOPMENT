using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
    [Header("Weapon")] 
    [SerializeField] SpriteRenderer sprWeapon;
    private WeaponHandler _weapon;
    
    [Header("Helmet")]
    [SerializeField] SpriteRenderer sprHelmet;
    private HelmetHandler _helmet;
    
    [Header("Shield")]
    [SerializeField] SpriteRenderer sprShield;
    private ShieldHandler _shield;

    
    //Weapon
    public void EquipWeapon(WeaponHandler weapon, Sprite weaponSpr)
    {
        _weapon = weapon;
        sprWeapon.sprite = weaponSpr;
        sprWeapon.transform.localPosition = weapon.initPosition;

    }
    public bool IsEquipWeapon => _weapon != null;

    public void DropWeapon()
    {
        _weapon = null;
        sprWeapon = null;
    }
    
    
    //Helmet
    public void EquipHelmet(HelmetHandler helmet, Sprite helmetSpr)
    {
        _helmet = helmet;
        sprHelmet.sprite = helmetSpr;

    }
    public bool IsEquipHelmet => _helmet != null;

    public void DropHelmet()
    {
        _helmet = null;
        sprHelmet = null;
    }
    
    
    //Shield
    public void EquipShield(ShieldHandler shield, Sprite shieldSpr)
    {
        _shield = shield;
        sprShield.sprite = shieldSpr;

    }
    public bool IsEquipShield => _shield != null;

    public void DropShield()
    {
        _shield = null;
        sprShield = null;
    }
}
