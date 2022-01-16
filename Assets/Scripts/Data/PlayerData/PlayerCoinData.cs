using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerCurrencyData
{
    public List<CurrencyData> currencyDatas;

    public void AddData(CurrenyEnum typeCurrency, int value)
    {
        if (currencyDatas.All(x => x.currenyEnum != typeCurrency))
        {
            currencyDatas.Add(new CurrencyData(typeCurrency,value));
        }
    }

    public CurrencyData GetData(CurrenyEnum type)
    {
        return currencyDatas.Find(x => x.currenyEnum == type);
    }

    public void OnSetupData()
    {
        currencyDatas.Add(new CurrencyData(CurrenyEnum.Coin,0));
        currencyDatas.Add(new CurrencyData(CurrenyEnum.Gem,0));
        currencyDatas.Add(new CurrencyData(CurrenyEnum.Mana,0));
        currencyDatas.Add(new CurrencyData(CurrenyEnum.Wood,0));
    }
}

[Serializable]
public class CurrencyData
{
    public CurrencyData(CurrenyEnum type, int value)
    {
        this.currenyEnum = type;
        this.value = value;
    }
    public CurrenyEnum currenyEnum;
    public int value;
}

public enum CurrenyEnum
{
    Coin = 1,
    Mana = 2,
    Gem = 3,
    Wood = 4
}