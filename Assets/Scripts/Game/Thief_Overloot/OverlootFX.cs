using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OverlootFX : MonoBehaviour
{
    [Header("Spark")]
    [SerializeField] private CanvasGroup root;
    [SerializeField] private Image rootFX;
    [SerializeField] private Image spark;

    [SerializeField] private List<Sprite> spr;

    [Header("Overloot char")] 
    [SerializeField] private Image over;
    [SerializeField] private Image loot;

    private void OnEnable()
    {
        root.DOFade(1, 0.05F).From(0);
    }

    IEnumerator ChangeSparkSpr()
    {
        float timeChange = 1.5f;
        float curTime = 0;
        int currentSpr = 0;
        int maxSpr = spr.Count;
        while (curTime < timeChange)
        {
            curTime += 0.1f;
            currentSpr++;
            currentSpr %= maxSpr;
            spark.sprite = spr[currentSpr];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator DOFx(float time)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(rootFX.transform.DOScale(1, 0.2F).From(0))
            .Join(over.transform.DOScale(1,0.3F).From(0).SetEase(Ease.OutBack).SetDelay(0.1F))
            .Join(loot.transform.DOScale(1,0.3F).From(0).SetEase(Ease.OutBack).SetDelay(0.1f));

        StartCoroutine(ChangeSparkSpr());
        
        yield return new WaitForSeconds(time);
        root.DOFade(0, 0.1F);
        yield return new WaitForSeconds(0.2F);
        this.gameObject.SetActive(false);
    }
}
