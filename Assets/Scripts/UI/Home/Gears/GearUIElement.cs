﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class GearUIElement : MonoBehaviour
{
    public GearType type;

    [Header("UI Config")] 
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private Image gearImage;
    
    #if UNITY_EDITOR

    private void OnValidate()
    {
        txtLevel = GetComponentInChildren<TextMeshProUGUI>();
    }

#endif
    
    private void OnEnable()
    {
        SetupGear();
    }


    public void SetupGear()
    {
        var gearData = PlayerDataManager.Instance.data.GearDatas;
        var data = gearData.GetDataByType(type);
        if (data != null)
        {
            var assets = GearItemAssets.Instance.GetAsset(type);
            if (assets != null)
            {
                gearImage.sprite = assets.imgRrepresent;
                txtLevel.text = data.level.ToString();
            }
        }
    }
    
    
}