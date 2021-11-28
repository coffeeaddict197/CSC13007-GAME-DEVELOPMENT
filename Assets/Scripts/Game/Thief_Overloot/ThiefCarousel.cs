using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThiefCarousel : MonoBehaviour
{
    [Header("Carousel")]
    [SerializeField] RectTransform carousel;

    [Header("Decor")] 
    [SerializeField] private RectTransform leftHand;
    [SerializeField] private RectTransform hook;
    

    public void Scroll(Action completAction)
    {
                
        //Unactive fx
        leftHand.GetChild(0).gameObject.SetActive(false);
        hook.GetChild(0).gameObject.SetActive(false);
        
        carousel.DOAnchorPosY(-615, 0.3f).SetLoops(12,LoopType.Restart).SetEase(Ease.Linear).OnComplete(() =>
        {
            this.MoveFX();
            completAction?.Invoke();
        });
    }

    public int GetCurrentRow()
    {
        return Mathf.RoundToInt(Mathf.Abs((585 - carousel.anchoredPosition.y) / GameGrid.CEIL_SIZE));
    }

    void MoveFX()
    {
        leftHand.DOAnchorPosX(2200, 0.8f);
        hook.DOAnchorPosX(-2200, 0.8f);
        
        //Active fx
        leftHand.GetChild(0).gameObject.SetActive(true);
        hook.GetChild(0).gameObject.SetActive(true);
    }
}
