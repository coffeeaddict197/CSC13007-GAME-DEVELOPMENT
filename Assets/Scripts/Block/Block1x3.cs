using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block1x3 : BaseBlock
{
    protected override void Awake()
    {
        base.Awake();
        blockItem.InitItem(blockData.blockType,"Sword");
    }

    public override bool CheckCanSpawnAt(GridNode node, GridNode[,] gameGrid, out List<GridNode> listNode)
    {
        listNode = new List<GridNode>();
        for (int i = 0; i < 3; i++)
        {
            if (node.row - i >= 0)
            {
                GridNode nodeCheck = gameGrid[node.row - i, node.col];
                if (!nodeCheck.isContainObject)
                {
                    listNode.Add(nodeCheck);
                }
            }
        }
        if (listNode.Count < 3)
        {
            return false;
        }
        return true;
    }
}
