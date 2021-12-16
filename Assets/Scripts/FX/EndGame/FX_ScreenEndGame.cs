using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
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
        rewardBGShower.gameObject.SetActive(true);
        rewardBGShower.SetTrigger("PlayResultBG");
        yield return new WaitForSeconds(1.2f);
        rewardShower.ActiveReward(new List<RewardInf>()
        {
            new RewardInf()
            {
                amount = 6521,
                rwType =  RewardType.Coin
            },
            new RewardInf()
            {
                amount = 200,
                rwType =  RewardType.Wood
            },
            
            new RewardInf()
            {
                amount = 542,
                rwType =  RewardType.Iron
            },
            new RewardInf()
            {
            amount = 340,
            rwType =  RewardType.Mana
        }
            
        });
    }
}
