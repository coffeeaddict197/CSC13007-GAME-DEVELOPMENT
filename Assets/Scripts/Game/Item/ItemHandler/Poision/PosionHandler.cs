using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionHandler : ItemHandler
{
    public BuffEnum buffType;
    public float timeCountDown;
    public int affectValue;
    public bool isActived;
    public Sprite representSpr;


        [HideInInspector]
    public float initTime;

    #if UNITY_EDITOR

    private void OnValidate()
    {
        initTime = timeCountDown;
    }

#endif

    public void ResetTime()
    {
        timeCountDown = initTime;
    }
    public override bool OnEquipItem(BaseBlock block)
    {
        return true;
    }

    public virtual bool OnDeBuff()
    {
        isActived = false;
        return true;
    }
    
    
    public static PosionHandler CreateInstance(PosionHandler copier)
    {
        var data = ScriptableObject.CreateInstance<PosionHandler>();
        data = copier;
        data.timeCountDown = copier.initTime;
        return data;
    }
}
