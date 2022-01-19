using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConcurrencySmith : UIConcurrency
{
    [SerializeField] private TextMeshProUGUI _textValueRequire;

    public static Action OnUpgradeLevel;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnUpgradeLevel += SetupCurrency;
    }

    private void OnDisable()
    {
        OnUpgradeLevel -= SetupCurrency;
    }

    protected override void SetupCurrency()
    {
        var playerData = PlayerDataManager.Instance.data;
        var data = playerData.currencyData.GetData(currenyEnum);
        if (data!=null)
        {
            _textValue.text = data.value.ToString() + "/";
            _textValueRequire.text = BlackSmithShopConfigs.Instance.GetConfigs(currenyEnum)
                .GetPriceByLevel(playerData.ItemBlackSmithLevel).ToString();
            
        }
    }
}
