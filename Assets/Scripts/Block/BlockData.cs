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
    private Action onChangeLevel;

    public void InitData(Action callBackChangeLevel = null)
    {
        onChangeLevel += callBackChangeLevel;
        BlockLevel = 1;
    }

    public int BlockLevel
    {
        get => _blockLevel;
        set
        {
            if (value >= _blockLevel)
            {
                onChangeLevel?.Invoke();
            }
        }
    }

    public void OnChangeLevel()
    {
        BlockBackgroundConfig bg = GameAssetsConfigs.Instance.blockBgConfigs.GetConfig(blockType, _blockLevel);
        blockImage.sprite = bg.spr;
    }
}