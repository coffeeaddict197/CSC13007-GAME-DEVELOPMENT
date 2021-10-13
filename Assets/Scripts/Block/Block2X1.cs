using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block2X1 : BaseBlock
{
    protected override void Awake()
    {
        base.Awake();
        blockItem.InitItem(blockData.blockType);
    }
    public override bool CheckCanSpawnAt(GridNode node, GridNode[,] gameGrid, out List<GridNode> listNode)
    {
        listNode = new List<GridNode>();
        for (int i = 0; i < 2; i++)
        {
            if (node.col + i < GameGrid.MAX_COL)
            {
                GridNode nodeCheck = gameGrid[node.row, node.col + i];
                if (!nodeCheck.isContainObject)
                {
                    listNode.Add(nodeCheck);
                }
            }
        }
        if (listNode.Count < 2)
        {
            return false;
        }
        return true;
    }
}
