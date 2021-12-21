using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemHandler : ScriptableObject
{
    public float durability;

    protected float _currentDurability;
    public virtual float CurrentDurability
    {
        get => _currentDurability;
        set
        {
            _currentDurability = value;
            ItemEquipSlot.DoFill(ItemType.Weapon,_currentDurability/durability, Math.Abs(_currentDurability - durability) < 0.1f);
        }
    }

    public void Init()
    {
        CurrentDurability = durability;
    }

    public abstract bool OnEquipItem(BaseBlock block);

}
