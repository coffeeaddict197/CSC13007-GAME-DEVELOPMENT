using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemHandler : ScriptableObject
{
    public abstract bool OnEquipItem(BaseBlock block);
   
}
