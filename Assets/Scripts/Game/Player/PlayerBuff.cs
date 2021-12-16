using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffEnum
{
    BuffShield,
    BuffAttack
}
public class PlayerBuff : MonoBehaviour
{
    public Dictionary<BuffEnum,SpellBuff> SpellBuffs;

    private List<SpellBuff> _currentBuffs;
    private void Awake()
    {
        SpellBuffs = new Dictionary<BuffEnum,SpellBuff>()
        {
            {BuffEnum.BuffShield,new ShieldBuff()}
        };
    }

    public int GetBuffAffectValue(BuffEnum buffEnum)
    {
        var buff = SpellBuffs[buffEnum];
        return buff != null ? buff.config.affectValue : 0;
    }

    public void OnBuff(BuffEnum buffEnum,PosionHandler config)
    {
        SpellBuff spell = SpellBuffs[buffEnum];
        spell.config = config;
        spell.OnBuff();
        StartCoroutine(CountDownSpell(spell));
    }

    IEnumerator CountDownSpell(SpellBuff buff)
    {
        float curTime = buff.config.timeCountDown;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            yield return null;
        }
        buff.OnDeBuff();
    }

    void AddToSpell(SpellBuff buff)
    {
        if (!_currentBuffs.Contains(buff))
        {
            _currentBuffs.Add(buff);
        }
    }

    
}

public abstract class SpellBuff
{
    public PosionHandler config;
    public abstract void OnBuff();
    public abstract void OnDeBuff();
}

[System.Serializable]
public class ShieldBuff : SpellBuff
{
    public override void OnBuff()
    {
        Debug.Log("Buffing");
    }

    public override void OnDeBuff()
    {
        Debug.Log("DeBuff");
    }
}
