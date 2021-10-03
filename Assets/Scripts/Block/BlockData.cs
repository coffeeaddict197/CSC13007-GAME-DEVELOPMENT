using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData
{
    [SerializeField] private int _blockLevel;

    public int BlockLevel
    {
        get => _blockLevel;
        //set; //Do some stuff: change sprite, etc..
    }
    
}
