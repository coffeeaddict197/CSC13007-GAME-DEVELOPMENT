using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block1X1 : BaseBlock
{
    protected override void Awake()
    {
        base.Awake();
        blockItem.InitItem(blockData.blockType);
    }

    public override bool CheckCanSpawnAt(GridNode node, GridNode[,] grid,out List<GridNode> listNode)
    {
        listNode = new List<GridNode>();
        GridNode nodeCheck = grid[node.row, node.col];
        if (!nodeCheck.isContainObject)
        {
            listNode.Add(node);
            return true;
        }
        return false;
    }
}