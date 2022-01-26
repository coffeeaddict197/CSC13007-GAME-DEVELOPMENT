using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    
    public void OnWatchAds()
    {
        Player.Instance.IsDeath = false;
        this.gameObject.SetActive(false);
    }

    public async void OnBackHome()
    {
        Time.timeScale = 1;
        Transition.Instance.PlayTransition("CloseAnim");
        await UniTask.DelayFrame(35);
        SceneManager.LoadSceneAsync(1);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        Transition.Instance.PlayTransition("OpenAnim");
    }

}
