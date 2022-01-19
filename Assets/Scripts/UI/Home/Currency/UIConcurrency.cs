using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConcurrency : MonoBehaviour
{
    public RewardType currenyEnum;

    [Header("UI")] 
    [SerializeField] protected TextMeshProUGUI _textValue;
    #if UNITY_EDITOR

    private void OnValidate()
    {
        _textValue = GetComponentInChildren<TextMeshProUGUI>();
    }

#endif
    protected virtual void OnEnable()
    {
        SetupCurrency();
    }

    protected virtual void SetupCurrency()
    {
        var data = PlayerDataManager.Instance.data.currencyData.GetData(currenyEnum);
        if (data!=null)
        {
            _textValue.text = data.value.ToString();
        }
    }
}
