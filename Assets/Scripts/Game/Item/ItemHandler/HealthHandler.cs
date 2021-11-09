using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemHandler/HealthBurger", fileName = "HelmetHandler")]
public class HealthHandler : ItemHandler
{
    public int buffHealth;
    
    public override bool OnEquipItem(BaseBlock block)
    {
        Player player = Player.Instance;
        player.CurrentHealth += buffHealth * block.blockData.BlockLevel;
        return true;
    }
}
