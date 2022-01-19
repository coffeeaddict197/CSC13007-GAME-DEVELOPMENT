using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface ICommondFX
{
    float timeWaitng { get; }
    float timeDoing { get; }
    int priority { get; }
    void DoFX();
}


public class FX_ScreenEndGame : MonoSingleton<FX_ScreenEndGame>
{
    public List<ICommondFX> commndFX;

    [Header("UI Reference")] 
    [SerializeField] private RectTransform parentScreenEndGame;
    [SerializeField] private Animator rewardBGShower;
    [SerializeField] private FX_RewardShower rewardShower;


    protected override void Awake()
    {
        base.Awake();
        commndFX = new List<ICommondFX>();
    }

    public void Excute()
    {
        StartCoroutine(ExecuteIEnumrator());
    }

    IEnumerator ExecuteIEnumrator()
    {
        commndFX =  commndFX.OrderBy(x => x.priority).ToList();
        for (int i = 0; i < commndFX.Count; i++)
        {
            ICommondFX fx = commndFX[i];
            yield return new WaitForSeconds(fx.timeWaitng);
            fx.DoFX();
            yield return new WaitForSeconds(fx.timeDoing);
        }
    }

    public void Add(ICommondFX fx)
    {
        if (!commndFX.Contains(fx))
        {
            commndFX.Add(fx);
        }
    }

    public void Remove(ICommondFX fx)
    {
        if (commndFX.Contains(fx))
        {
            commndFX.Remove(fx);
        }
    }
    
    public void DoFX()
    {
        parentScreenEndGame.gameObject.SetActive(true);
        parentScreenEndGame.anchoredPosition = new Vector2(2500f, 0f);
        parentScreenEndGame.DOAnchorPosX(0, 0.3f).SetEase(Ease.Linear);
        StartCoroutine(FXBGResult());

    }

    IEnumerator FXBGResult()
    {
        yield return new WaitForSeconds(0.6f);
        rewardBGShower.gameObject.SetActive(true);
        rewardBGShower.SetTrigger("PlayResultBG");
        yield return new WaitForSeconds(1.2f);

        List<RewardInf> listReward = SetupReward();
        
        
        rewardShower.ActiveReward(listReward);
        AddReward(listReward);
        yield return new WaitForSeconds(0.6f);
        BackHome();
    }

    List<RewardInf> SetupReward()
    {
        List<RewardInf> rewards = new List<RewardInf>();
        int curLevel = ButtonLevel.buttonLevelClicked.level;
        var rwData = LevelAssetsConfigs.Instance.GetLevel(curLevel).rewardData;
        rewards.Add( new RewardInf()
        {
            amount = rwData.coin,
            rwType =  RewardType.Coin
        });
        rewards.Add( new RewardInf()
        {
            amount = rwData.mana,
            rwType =  RewardType.Mana
        });
        rewards.Add( new RewardInf()
        {
            amount = rwData.silver,
            rwType =  RewardType.Iron
        });
        rewards.Add( new RewardInf()
        {
            amount = rwData.wood,
            rwType =  RewardType.Wood
        });

        return rewards;

    }
    void AddReward(List<RewardInf> rewards)
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            PlayerDataManager.Instance.data.currencyData.AddData(rewards[i].rwType,rewards[i].amount);
        }

        PlayerDataManager.Instance.data.LevelDatas.archiveLevel++;
    }

    async void BackHome()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: this.GetCancellationTokenOnDestroy());
        Transition.Instance.PlayTransition("CloseAnim");
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SceneManager.LoadSceneAsync(1);
    }
}
