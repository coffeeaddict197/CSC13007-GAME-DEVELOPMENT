using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionHandler : ItemHandler
{
    public float timeCountDown;
    public int affectValue;
    
    public override bool OnEquipItem(BaseBlock block)
    {
        return true;
    }
}
