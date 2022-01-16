using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GearType typeOfGear;

    [SerializeField] private TextMeshProUGUI _textValue;
    [SerializeField] private TextMeshProUGUI _textStats;
    [SerializeField] private Image _imageStats; 
    #if UNITY_EDITOR

    private void OnValidate()
    {
        _textStats = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _textValue = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _imageStats = GetComponentInChildren<Image>();
    }
#endif

    private void OnEnable()
    {
        ReUpdateStats(typeOfGear);
        GearUpgradeDialog.OnGearClick += ReUpdateStats;
    }

    private void OnDisable()
    {
        GearUpgradeDialog.OnGearClick -= ReUpdateStats;
    }


    void ReUpdateStats(GearType typeOfGear)
    {
        if (this.typeOfGear != typeOfGear)
            return;
        var assets = GearItemAssets.Instance.GetAsset(typeOfGear);
        
        int levelGear = 1;
        var data = PlayerDataManager.Instance.data.GearDatas.GetDataByType(typeOfGear);
        if (data != null)
            levelGear = data.level;
        
        _textStats.text = assets.stats.statsEnum.ToString();
        int currentStat = assets.valueAfterLevelup * levelGear;
        _textValue.text = currentStat.ToString();
        _imageStats.sprite = assets.stats.statsImg;

    }
}
