using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Ultility
{
    static public Vector2 GetAveragePoint(List<GridNode> listNode)
    {
        Vector2 anchorPoint = Vector2.zero;
        foreach (var node in listNode)
        {
            anchorPoint.x += node.rect.anchoredPosition.x;
            anchorPoint.y += node.rect.anchoredPosition.y;
        }

        anchorPoint /= listNode.Count;
        return anchorPoint;
    }

    static public int RandomIn(int value, int min, int max)
    {
        min = value - 5;
        max = value + 5;
        return Random.Range(min, max);
    }
    
    
    public static Tween DOFade(this UIEffect fx, float target, float time, int loopTimes, LoopType type)
    {
        fx.colorFactor = 0;
        return DOTween.To(() => fx.colorFactor, value => fx.colorFactor = value, target, time).SetLoops(loopTimes,type).OnUpdate(() =>
        {
            fx.colorFactor = 0;
        });
    }
}