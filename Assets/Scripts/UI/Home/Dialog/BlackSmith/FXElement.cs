using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ElementFX
{
    public List<UIEffect> weaponsfx;
   public List<Image> fadeHorizontal;
   public List<TextMeshProUGUI> textFX;

    
}
public class FXElement : MonoBehaviour
{
    [Header("Refencer")] 
    [SerializeField] Image fadeVerticle;
    [SerializeField] private ElementFX _fx;
    [SerializeField] UIShiny shiny;


    [SerializeField] private ItemHandler _handler;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        _fx.weaponsfx = GetComponentsInChildren<UIEffect>().ToList();
    }
#endif
    public async void DOFX()
    {
        StartFade(fadeVerticle, .7f);
        FXWeapon();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f),cancellationToken:this.GetCancellationTokenOnDestroy());
        for (int i = 0; i < _fx.fadeHorizontal.Count; i++)
        {
            StartFade(_fx.fadeHorizontal[i], .2f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f),cancellationToken:this.GetCancellationTokenOnDestroy());
        }
    }

    private void OnEnable()
    {
        
    }

    async void FXWeapon()
    {
        for (int i = 0; i < _fx.weaponsfx.Count; i++)
        {
            _fx.textFX[i].text = (_handler.valueByLevel[i] +
                            _handler.valueIncrease * PlayerDataManager.Instance.data.ItemBlackSmithLevel).ToString();
            Sequence seq = DOTween.Sequence();
            seq.Join(_fx.weaponsfx[i].DOFade(0.7f, 0.5f, 2, LoopType.Yoyo))
                .Join(_fx.weaponsfx[i].transform.DOScale(1.5f, 0.5f).From(1).SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutBack))
                .Join(_fx.textFX[i].DOFade(1, 0.5f).From(0).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo));
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: this.GetCancellationTokenOnDestroy());
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: this.GetCancellationTokenOnDestroy());
        shiny.Play();
    }

    Tween StartFade(Image img, float fadeTo)
    {
        return img.DOFade(fadeTo, 0.4f).From(0).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo);
    }

    // public List<int> GetCurrentStats()
    // {
    //     if (weapon != null)
    //     {
    //         return weapon.damageByLevel;
    //     }
    //
    //     if (helmet != null)
    //     {
    //         
    //     }
    //
    //     if (shield != null)
    //     {
    //         
    //     }
    // }

}
