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
        player.spellBuff.OnBuff(this);
        PlayerFX.Instance.FXPlayShieldPoison();
        SoundManager.Instance.Play("Poision_Shield",AudioType.FX,0.6f);
        return true;
    }
    
    
    public override bool OnDeBuff()
    {
        PlayerFX.Instance.FXStopShieldPoison();
        return true;
        
    }
}
