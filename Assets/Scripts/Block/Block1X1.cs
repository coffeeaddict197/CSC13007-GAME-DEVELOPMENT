using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block1X1 : BaseBlock
{
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

    public override void ReUpdateGrid()
    {
        throw new System.NotImplementedException();
    }
}