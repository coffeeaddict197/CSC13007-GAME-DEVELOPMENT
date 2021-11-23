using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BlockData
{
    public BlockType blockType;
    [SerializeField] private Image blockImage;
    [SerializeField] private int _blockLevel;
    private Action<int> onChangeLevel;

    public void InitData(Action<int> callBackChangeLevel = null)
    {
        onChangeLevel += OnChangeLevel;
        onChangeLevel += callBackChangeLevel;
        BlockLevel = 1;
    }
    

    public int BlockLevel
    {
        get => _blockLevel;
        set
        {
           onChangeLevel?.Invoke(value);
        }
    }

    public void OnChangeLevel(int level)
    {
        if (level > 4)
            return;
        if (level >= _blockLevel)
        {
            BlockBackgroundConfig bg = GameAssetsConfigs.Instance.blockBgConfigs.GetConfig(blockType, level);
            blockImage.sprite = bg.spr;
            _blockLevel = level;
        }
    }
}