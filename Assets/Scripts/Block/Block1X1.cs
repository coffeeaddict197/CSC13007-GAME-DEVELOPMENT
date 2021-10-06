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

    // public override bool ReUpdateGrid()
    // {
    //     //Setup
    //     int downStep = GetDownStep();
    //
    //     if (downStep <= 0)
    //         return false;
    //     
    //     GameGrid gameGrid = GameGrid.Instance;
    //
    //     
    //     GridNode oldStartNode = gridContains[0];
    //     GridNode newStartNode = gameGrid.grid[oldStartNode.row + downStep , oldStartNode.col];
    //     List<GridNode> newGrid = new List<GridNode>();
    //
    //     CheckCanSpawnAt(newStartNode, gameGrid.grid, out newGrid);
    //     
    //     this.ResetGridContain();
    //     gridContains = newGrid;
    //
    //     return true;
    //
    // }
}