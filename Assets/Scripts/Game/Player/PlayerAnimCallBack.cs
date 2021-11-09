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
        
        Vector3 posTarget = MonsterManager.Instance.GetCurrentMonster().GetAnchorPosition();
        FXFactory.Instance.fxTextFactory.SpawnFX(posTarget,damage.ToString());
    }
}
