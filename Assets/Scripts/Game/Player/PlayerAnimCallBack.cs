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
        if(MonsterManager.Instance.IsMonsterAvailble())
            Player.onPlayerTakeDamagae?.Invoke(gears.TakeDamage());
        else
            anim.Reset();
    }
}
