using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill",fileName = "Skill")]
public class SkillHandler : ItemHandler
{
    public int damage;
    public override bool OnEquipItem(BaseBlock block)
    {
        if (damage > 0)
        {
            var monster = MonsterManager.Instance.GetCurrentMonster();
            if (monster != null)
            {
                monster.Health -= damage;
                SoundManager.Instance.Play("FireBooster",AudioType.FX,0.6f);
                MonsterFX.Instance.FXPlayMonsterTakeDamage();
                FXFactory.Instance.GetFXTextFactory()
                    .SpawnFX(monster.GetAnchorPosition(), damage.ToString(), FXTextFactory.damageColor);
                return true;
            }
            
        }

        return false;
    }
}