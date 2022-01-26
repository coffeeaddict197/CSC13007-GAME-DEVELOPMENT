using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgradeDialog : BaseDialog
{
    private static int PriceAfterUpLevel = 15;
    private static int defaultPrice = 50;
    
    [Header("UI Configs")] 
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI currentStats;
    [SerializeField] private TextMeshProUGUI NextStats;
    [SerializeField] private TextMeshProUGUI stats;
    [SerializeField] private TextMeshProUGUI currentPrice;
    [SerializeField] private Image imgStats;

    [Header("Buttons")] 
    [SerializeField] private Button onUpgrade;
    [SerializeField] private Button onQuit;
    public static Action<GearType> OnGearClick;

    private GearData _currentGear;
    private GearUIElement _currentUIElement;

    private int _price;
    private void Start()
    {
        onUpgrade.onClick.AddListener(OnUpgrade);
        
        onQuit.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        OnGearClicked(GearType.Amulet);
        OnGearClick += OnGearClicked;

    }

    private void OnDisable()
    {
        OnGearClick -= OnGearClicked;
    }

    void OnGearClicked(GearType typeOfGear)
    {
        var assets = GearItemAssets.Instance.GetAsset(typeOfGear);
        var data = PlayerDataManager.Instance.data.GearDatas.GetDataByType(typeOfGear);
        int gearLevel = data !=null ? data.level : 1;
        _price = gearLevel * PriceAfterUpLevel + defaultPrice;
        
        name.text = assets.name;
        int currentStat = assets.valueAfterLevelup * gearLevel;
        currentStats.text = currentStat.ToString();
        NextStats.text = (currentStat + assets.valueAfterLevelup).ToString();
        stats.text = assets.stats.statsEnum.ToString();
        currentPrice.text = _price.ToString();
        imgStats.sprite = assets.stats.statsImg;
        _currentGear = data;
    }

    void OnUpgrade()
    {
        var currentDataCoin = PlayerDataManager.Instance.data.currencyData.GetData(RewardType.Coin);
        if (currentDataCoin.value < _price)
            return;

        currentDataCoin.value -= _price;
            SoundManager.Instance.Play("Combine4",AudioType.FX,0.6f);
        _currentGear.level++;
        OnGearClick(_currentGear.type);
        GearUIElement.OnUpdateLevel?.Invoke(_currentGear.type);
    }
}
