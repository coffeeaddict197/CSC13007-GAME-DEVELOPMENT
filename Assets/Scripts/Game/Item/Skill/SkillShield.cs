using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "SkillShield",fileName = "Skill")]
public class SkillShield : ItemHandler
{
    public override bool OnEquipItem(BaseBlock block)
    {
        PlayerGear.EquipedShieldBooster = true;
        CountDown();
        return true;
    }

    async void CountDown()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(10));
        PlayerGear.EquipedShieldBooster = false;
    }
}
