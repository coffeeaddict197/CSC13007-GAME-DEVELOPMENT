using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block2X2 : BaseBlock
{
    public override bool CheckCanSpawnAt(GridNode node, GridNode[,] gameGrid, out List<GridNode> listNode)
    {
        listNode = new List<GridNode>();
        CheckAndAdd(ref listNode, node);
        if (node.row - 1 >= 0)
        {
            GridNode nodeCheck = gameGrid[node.row - 1, node.col];
            CheckAndAdd(ref listNode, nodeCheck);
        }
        
        if (node.row - 1 >= 0 && node.col + 1 < GameGrid.MAX_COL)
        {
            GridNode nodeCheck = gameGrid[node.row - 1, node.col + 1];
            CheckAndAdd(ref listNode, nodeCheck);
        }
        
        if (node.col + 1 < GameGrid.MAX_COL)
        {
            GridNode nodeCheck = gameGrid[node.row, node.col + 1];
            CheckAndAdd(ref listNode, nodeCheck);
        }
        
        if (listNode.Count >= 4)
        {
            return true;
        }

        return false;
    }

    public void CheckAndAdd(ref List<GridNode> res, GridNode node)
    {
        if (!node.isContainObject)
        {
            res.Add(node);
        }
    }
}
