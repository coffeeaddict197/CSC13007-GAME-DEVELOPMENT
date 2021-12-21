using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffEnum
{
    BuffShield,
    BuffAttack
}
// public class PlayerBuff : MonoBehaviour
// {
//     public Dictionary<BuffEnum,SpellBuff> SpellBuffs;
//
//     private List<SpellBuff> _currentBuffs;
//     private void Awake()
//     {
//         SpellBuffs = new Dictionary<BuffEnum,SpellBuff>()
//         {
//             {BuffEnum.BuffShield,new ShieldBuff()}
//         };
//     }
//
//     public int GetBuffAffectValue(BuffEnum buffEnum)
//     {
//         var buff = SpellBuffs[buffEnum];
//         return buff != null ? buff.config.affectValue : 0;
//     }
//
//     public void OnBuff(BuffEnum buffEnum,PosionHandler config)
//     {
//         SpellBuff spell = SpellBuffs[buffEnum];
//         spell.config = config;
//         spell.OnBuff();
//         StartCoroutine(CountDownSpell(spell));
//     }
//
//     IEnumerator CountDownSpell(SpellBuff buff)
//     {
//         float curTime = buff.config.timeCountDown;
//         while (curTime > 0)
//         {
//             curTime -= Time.deltaTime;
//             yield return null;
//         }
//         buff.OnDeBuff();
//     }
//
//     void AddToSpell(SpellBuff buff)
//     {
//         if (!_currentBuffs.Contains(buff))
//         {
//             _currentBuffs.Add(buff);
//         }
//     }
//
//     
// }
//
// public abstract class SpellBuff
// {
//     public PosionHandler config;
//     public abstract void OnBuff();
//     public abstract void OnDeBuff();
// }
//
// [System.Serializable]
// public class ShieldBuff : SpellBuff
// {
//     public override void OnBuff()
//     {
//         PlayerFX.Instance.FXPlayShieldPoison();
//         
//     }
//
//     public override void OnDeBuff()
//     {
//         Debug.Log("DeBuff");
//     }
// }

public class PlayerBuff : MonoBehaviour
{
    private List<PosionHandler> SpellBuffs;

    [Header("UI Reference")]
    public List<UIBuffElement> BuffElements;
    private void Awake()
    {
        SpellBuffs = new List<PosionHandler>();
    }

    public int GetBuffAffectValue(BuffEnum buffEnum)
    {
        var buff = SpellBuffs.Find(x => x.buffType == buffEnum);
        return buff != null ? buff.affectValue : 0;
    }

    public void OnBuff(PosionHandler config)
    {
        PosionHandler newInstance = PosionHandler.CreateInstance(config);
        var buff = SpellBuffs.Find(x => x.buffType == newInstance.buffType);
        var UIBuffElement = BuffElements.Find(x => x.buffType == config.buffType);
        UIBuffElement.gameObject.SetActive(true);
        if (buff != null)
        {
            if(buff.isActived)
            {
                buff.ResetTime();
            }
        }
        else
        {
            AddToSpell(newInstance);
            buff = newInstance;
            StartCoroutine(CountDownSpell(buff,UIBuffElement));
        }
        
        
        // PosionHandler newInstance = PosionHandler.CreateInstance(config);
        // var buff = SpellBuffs.Find(x => x.buffType == newInstance.buffType);
        // if (buff != null)
        // {
        //     var UIBuffElement = BuffElements.Find(x => x.buffType == config.buffType);
        //     UIBuffElement.gameObject.SetActive(true);
        //     if(buff.isActived)
        //     {
        //         buff.ResetTime();
        //     }
        //     else
        //     {
        //         StartCoroutine(CountDownSpell(buff,UIBuffElement));
        //     }
        // }
        //
        // AddToSpell(newInstance);

    }

    IEnumerator CountDownSpell(PosionHandler config,UIBuffElement UI)
    {
        config.isActived = true;
        while (config.timeCountDown > 0)
        {
            config.timeCountDown -= Time.deltaTime;
            UI.Fill(config.timeCountDown/config.initTime);
            yield return null;
        }

        config.OnDeBuff();
        UI.gameObject.SetActive(false);
        SpellBuffs.Remove(config);
    }

    void AddToSpell(PosionHandler buff)
    {
        if (!SpellBuffs.Contains(buff))
        {
            SpellBuffs.Add(buff);
        }
    }

    
}

