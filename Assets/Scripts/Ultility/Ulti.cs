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

}
