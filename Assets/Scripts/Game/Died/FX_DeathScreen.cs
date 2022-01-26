using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FX_DeathScreen : MonoSingleton<FX_DeathScreen>
{
    [Header("Reference")] 
    [SerializeField] private Image bg;

    [SerializeField] private Transform pivotScale;

    [SerializeField] private Button watchAds;
    [SerializeField] private Button goBack;

    private void Start()
    {
        watchAds.onClick.AddListener(OnWatchAds);
        goBack.onClick.AddListener(OnBackHome);
    }

    private void OnEnable()
    {
        DOFX();
    }

    public void DOFX()
    {
        pivotScale.transform.localScale = new Vector3(1, 0,1);
        Sequence seq = DOTween.Sequence();
        seq.Append(bg.DOFade(0.7f, 1.5f).From(0))
            .Append(pivotScale.transform.DOScaleY(1, 0.3f).SetEase(Ease.OutBack));
    }

    void OnWatchAds()
    {
        Player.Instance.IsDeath = false;
        this.gameObject.SetActive(false);
    }

    async void OnBackHome()
    {
        Transition.Instance.PlayTransition("CloseAnim");
        await UniTask.DelayFrame(35);
        SceneManager.LoadSceneAsync(2);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        Transition.Instance.PlayTransition("OpenAnim");
    }


}
