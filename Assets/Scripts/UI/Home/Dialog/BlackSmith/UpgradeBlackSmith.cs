using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBlackSmith : MonoBehaviour
{
    const int COIN_UPGRADE = 250;
    [SerializeField] private Button _btn;

    private void Start()
    {
        _btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        var playerData = PlayerDataManager.Instance.data;
        if (CanUpgrade())
        {
            SoundManager.Instance.Play("Upgrade_BlackSmith",AudioType.FX,0.6f);
            playerData.ItemBlackSmithLevel++;
            ISmithLevelBooster.OnLevelUp?.Invoke();
            FXUpgradeClicked.Instance.StartFX();
            
        }
    }

    bool CanUpgrade()
    {
        var playerData = PlayerDataManager.Instance.data;
        var coinData = playerData.currencyData.GetData(RewardType.Coin);
        var woodData = playerData.currencyData.GetData(RewardType.Wood);
        var ironData = playerData.currencyData.GetData(RewardType.Iron);
        var manaData = playerData.currencyData.GetData(RewardType.Mana);

        var woodRequire = BlackSmithShopConfigs.Instance.GetConfigs(RewardType.Wood).GetPriceByLevel(playerData.ItemBlackSmithLevel);
        var ironRequire = BlackSmithShopConfigs.Instance.GetConfigs(RewardType.Iron).GetPriceByLevel(playerData.ItemBlackSmithLevel);
        var manaRequire = BlackSmithShopConfigs.Instance.GetConfigs(RewardType.Mana).GetPriceByLevel(playerData.ItemBlackSmithLevel);

        if (coinData.value >= COIN_UPGRADE 
            && woodData.value >= woodRequire 
            && ironData.value >= ironRequire 
            && manaData.value >= manaRequire)
        {
            coinData.value -= COIN_UPGRADE;
            ironData.value -= ironRequire;
            manaData.value -= manaRequire;
            woodData.value -= woodRequire;
            UIConcurrencySmith.OnUpgradeLevel?.Invoke();
            return true;
        }

        return false;
    }
    
    /*
     *
     * var playerData = PlayerDataManager.Instance.data;
        var data = playerData.currencyData.GetData(currenyEnum);
        if (data!=null)
        {
            _textValue.text = data.value.ToString() + "/";
            _textValueRequire.text = BlackSmithShopConfigs.Instance.GetConfigs(currenyEnum)
                .GetPriceByLevel(playerData.ItemBlackSmithLevel).ToString();
     * 
     */
}
