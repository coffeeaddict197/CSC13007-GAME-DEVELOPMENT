using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SettingPanelInGame : MonoSingleton<SettingPanelInGame>
{

    public Sprite bgToggleOff;
    public Sprite bgToggleOn;
    
    private void OnEnable()
    {
        this.transform.DOScale(1, 0.5F).From(0).SetEase(Ease.OutBack).SetUpdate(true);
        Time.timeScale = 0;
    }

    public void OnHide()
    {
        Time.timeScale = 1;
        this.transform.DOScale(0, 0.5F).From(1).SetEase(Ease.InBack).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
