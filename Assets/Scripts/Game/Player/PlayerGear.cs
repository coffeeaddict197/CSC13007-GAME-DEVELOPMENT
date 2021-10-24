using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
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
        sprWeapon.sprite = weaponSpr;
        sprWeapon.transform.localPosition = weapon.initPosition;

    }
    public bool IsEquipWeapon => _weapon != null;

    public void DropWeapon()
    {
        _weapon = null;
        sprWeapon.sprite = null;
    }

    public int TakeDamage()
    {
        if (_weapon != null)
        {
            _weapon.durability -= 5;
            if(_weapon.durability <= 0)
            {
                DropWeapon();
                return 5;
            }
            return _weapon.damage;
        }

        return 5;
    }
    
    
    //Helmet
    public void EquipHelmet(HelmetHandler helmet, Sprite helmetSpr)
    {
        anim.SetLayerWeight(1,1f);
        _helmet = HelmetHandler.CreateInstance(helmet);
        sprHelmet.sprite = helmetSpr;

    }
    public bool IsEquipHelmet => _helmet != null;

    public void DropHelmet()
    {
        anim.SetLayerWeight(1,0f);
        _helmet = null;
        sprHelmet = null;
    }
    
    
    //Shield
    public void EquipShield(ShieldHandler shield, Sprite shieldSpr)
    {
        _shield = ShieldHandler.CreateInstance(shield);
        sprShield.sprite = shieldSpr;

    }
    public bool IsEquipShield => _shield != null;

    public void DropShield()
    {
        _shield = null;
        sprShield = null;
    }
}
