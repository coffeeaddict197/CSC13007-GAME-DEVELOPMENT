using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerCurrencyData
{
    public List<CurrencyData> currencyDatas;

    public void AddData(RewardType typeCurrency, int value)
    {
        if (currencyDatas.Any(x => x.type == typeCurrency))
        {
            GetData(typeCurrency).value += value;
        }
    }

    public CurrencyData GetData(RewardType type)
    {
        return currencyDatas.Find(x => x.type == type);
    }

    public void OnSetupData()
    {
        currencyDatas.Add(new CurrencyData(RewardType.Coin,0));
        currencyDatas.Add(new CurrencyData(RewardType.Iron,0));
        currencyDatas.Add(new CurrencyData(RewardType.Mana,0));
        currencyDatas.Add(new CurrencyData(RewardType.Wood,0));
    }
}

[Serializable]
public class CurrencyData
{
    public CurrencyData(RewardType type, int value)
    {
        this.type = type;
        this._value = value;
    }

    public RewardType type;

   [SerializeField]  private int _value;
    public int value
    {
        get => _value;
        set
        {
            _value = value;
            IBooster.OnBoosterChange?.Invoke(type);
        }
    }
}

