using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InGameBooster : MonoBehaviour
{
    [SerializeField] private ItemHandler _handler;
    [SerializeField] private ParticleSystem fx;
    [SerializeField] private Image fill;

    public int Price;
    private bool canInteract = true;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (!canInteract)
            return;
        
        var currentDataCoin = PlayerDataManager.Instance.data.currencyData.GetData(RewardType.Coin);
        if (currentDataCoin.value < Price)
            return;
        currentDataCoin.value -= Price;
        fx.Play();
        _handler.OnEquipItem(null);
        CountDown();
    }

    
    async void CountDown()
    {
        canInteract = false;
        float timeDelay = 15f;
        while (timeDelay >= 0)
        {
            fill.fillAmount = timeDelay / 15f;
            timeDelay -= Time.deltaTime;
            await UniTask.Yield();
        }
        fill.fillAmount = 0;
        canInteract = true;
    }
}
