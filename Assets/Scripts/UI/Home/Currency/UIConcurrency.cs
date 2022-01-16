using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConcurrency : MonoBehaviour
{
    public CurrenyEnum currenyEnum;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _textValue;
    #if UNITY_EDITOR

    private void OnValidate()
    {
        _textValue = GetComponentInChildren<TextMeshProUGUI>();
    }

#endif
    private void OnEnable()
    {
        SetupCurrency();
    }

    void SetupCurrency()
    {
        var data = PlayerDataManager.Instance.data.currencyData.GetData(currenyEnum);
        if (data!=null)
        {
            _textValue.text = data.value.ToString();
        }
    }
}
