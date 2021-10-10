using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block1X2 : BaseBlock
{
    public override bool CheckCanSpawnAt(GridNode node, GridNode[,] gameGrid, out List<GridNode> listNode)
    {
        listNode = new List<GridNode>();
        for (int i = 0; i < 2; i++)
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
        if (listNode.Count < 2)
        {
            return false;
        }
        return true;
    }
}
