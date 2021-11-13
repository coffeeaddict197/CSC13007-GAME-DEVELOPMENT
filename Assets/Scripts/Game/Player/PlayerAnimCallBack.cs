using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCallBack : MonoBehaviour
{
    [Header("Refence")]
    [SerializeField] PlayerGear gears;
    [SerializeField] private PlayerAnim anim;
    
    public void OnAttack()
    {
        int damage = gears.TakeDamage();
        Player.onPlayerDamage?.Invoke(damage);
    }
}
