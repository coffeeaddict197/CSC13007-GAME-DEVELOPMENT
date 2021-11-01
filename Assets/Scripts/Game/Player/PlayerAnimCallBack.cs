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
        Player.onPlayerDamage?.Invoke(gears.TakeDamage());
    }
}
