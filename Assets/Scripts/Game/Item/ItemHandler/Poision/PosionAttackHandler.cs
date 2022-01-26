using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemHandler/Posion/AttackPosion", fileName = "AttackPosion")]
public class PosionAttackHandler : PosionHandler
{
    public override bool OnEquipItem(BaseBlock block)
    {
        //DOFX
        //Active Posion
        Player player = Player.Instance;
        player.spellBuff.OnBuff(this);
        PlayerFX.Instance.FXPlayAttackPoison();
        SoundManager.Instance.Play("Poision_Attack",AudioType.FX,0.6f);
        return true;
    }

    public override bool OnDeBuff()
    {
        PlayerFX.Instance.FXStopAttackPoison();
        return true;
        
    }
}