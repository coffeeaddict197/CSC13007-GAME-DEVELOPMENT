using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemHandler/Posion/ShieldPosion", fileName = "ShieldPosion")]
public class PosionShieldHandler : PosionHandler
{
    public override bool OnEquipItem(BaseBlock block)
    {
        //DOFX
        //Active Posion
        Player player = Player.Instance;
        player.spellBuff.OnBuff(BuffEnum.BuffShield,this);
        return true;
    }
}
