using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class GearUIElement : MonoBehaviour
{
    public GearType type;

    [Header("UI Config")] [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private Image gearImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image statsImage;

    public static Action OnUpdateLevel;
#if UNITY_EDITOR
    private void OnValidate()
    {
        txtLevel = GetComponentInChildren<TextMeshProUGUI>();
        backgroundImage = GetComponent<Image>();
        statsImage = transform.GetChild(2).GetComponent<Image>();
    }
#endif

    private void Start()
    {
        var btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => { GearUpgradeDialog.OnGearClick?.Invoke(type); });
        }
    }


    private void OnEnable()
    {
        SetupGear();

        OnUpdateLevel += SetupGear;
    }


    private void OnDisable()
    {
        OnUpdateLevel -= SetupGear;
    }

    public void SetupGear()
    {
        var gearData = PlayerDataManager.Instance.data.GearDatas;
        var data = gearData.GetDataByType(type);
        int gearLevel = data !=null ? data.level : 1;
        var assets = GearItemAssets.Instance.GetAsset(type);
        if (assets != null)
        {
            int blockLevel = gearLevel / 5 + 1;
            blockLevel = Mathf.Clamp(blockLevel, 1, 4);
            var spriteAsset = GameAssetsConfigs.Instance.blockBgConfigs.GetConfig(BlockType.Block_1x1, blockLevel);
            gearImage.sprite = assets.imgRrepresent;
            txtLevel.text = gearLevel.ToString();
            backgroundImage.sprite = spriteAsset.spr;
            statsImage.sprite = assets.stats.statsImg;
        }
    }

    void OnButtonClick()
    {
    }
}