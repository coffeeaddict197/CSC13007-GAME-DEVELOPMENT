using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}