using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType
{
    Coin,
    Wood,
    Iron,
    Mana
}
public class FX_RewardShower : MonoBehaviour
{
    public List<RewardItem> RewardItems;

    public void ParseData(List<RewardInf> rewardInfs)
    {
        for (int i = 0; i < RewardItems.Count; i++)
        {
            if (i < rewardInfs.Count)
            {
                RewardItems[i].ParseData(rewardInfs[i]);
                RewardItems[i].ROOT.SetActive(true);
                RewardItems[i].ROOT.transform.localScale = Vector3.zero;
            }
            else
            {
                RewardItems[i].ROOT.SetActive(false);
            }
        }
    }

    public void ActiveReward(List<RewardInf> rewardInfs)
    {
        ParseData(rewardInfs);  
        StartCoroutine(FXReward());
    }

    IEnumerator FXReward()
    {
        for (int i = 0; i < RewardItems.Count; i++)
        {
            if (RewardItems[i].ROOT.activeSelf)
            {

                RewardItems[i].ROOT.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.2F);
            }
        }
    }
}

[System.Serializable]
public class RewardItem
{
    public GameObject ROOT;
    public Image img;
    public TextMeshProUGUI text;
    
    private RewardInf _rewardInf;
    
    public void ParseData(RewardInf rewardInf)
    {
        _rewardInf = new RewardInf();
        this._rewardInf.rwType = rewardInf.rwType;
        this._rewardInf.amount = rewardInf.amount;
        img.sprite = LoaderUtility.Instance.GetAsset<Sprite>($"UI/Reward/{rewardInf.rwType.ToString()}");
        text.text = rewardInf.amount.ToString();
    }

    void AddToData()
    {
        
    }
}

public class RewardInf
{
    public RewardType rwType;
    public int amount;
}