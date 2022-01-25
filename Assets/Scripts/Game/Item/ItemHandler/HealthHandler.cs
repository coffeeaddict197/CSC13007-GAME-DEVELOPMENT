using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ItemHandler/HealthBurger", fileName = "HelmetHandler")]
public class HealthHandler : ItemHandler
{
    public int buffHealth;
    
    public override bool OnEquipItem(BaseBlock block)
    {
        int level = block != null ? block.blockData.BlockLevel : 1;
        Player player = Player.Instance;
        player.OnPlayerHealth(buffHealth);
        player.CurrentHealth += buffHealth * level;
        return true;
    }
}
