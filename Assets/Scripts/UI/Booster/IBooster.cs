using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class IBooster : MonoBehaviour
{
    public RewardType boosterType;

    public static Action<RewardType> OnBoosterChange;

    [SerializeField] private TextMeshProUGUI text;
    
    #if UNITY_EDITOR

    private void OnValidate()
    {
        text = GetComponent<TextMeshProUGUI>();
    }


#endif
    private void OnEnable()
    {
        OnBoosterChange += OnBoosterChanged;
    }

    private void OnDisable()
    {
        OnBoosterChange += OnBoosterChanged;
    }

    void OnBoosterChanged(RewardType type)
    {
        if (type != boosterType)
            return;
        
        var data = PlayerDataManager.Instance.data.currencyData.GetData(boosterType);
        text.text = data.value.ToString();
    }
    
    
    
}
