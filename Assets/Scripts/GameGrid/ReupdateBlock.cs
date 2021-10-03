using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReupdateBlock : MonoBehaviour
{
    //Nen chia subclass khi lam that
    public BlockType blockType;

    public void ReUpdate2X2(Block block, GridNode oldNodeStart, int moveDownStep)
    {
        block.ResetPosition();
        GameGrid gameGrid = GameGrid.Instance;

        List<GridNode> res = new List<GridNode>();
        GridNode startNode = gameGrid.grid[oldNodeStart.row + moveDownStep , oldNodeStart.col];
        
        gameGrid.CheckAndAdd(ref res, startNode);
        if (startNode.row - 1 >= 0)
        {
            GridNode nodeCheck = gameGrid.grid[startNode.row - 1, startNode.col];
            gameGrid.CheckAndAdd(ref res, nodeCheck);
        }
        
        if (startNode.row - 1 >= 0 && startNode.col + 1 < GameGrid.MAX_COL)
        {
            GridNode nodeCheck = gameGrid.grid[startNode.row - 1, startNode.col + 1];
            gameGrid.CheckAndAdd(ref res, nodeCheck);
        }
        
        if (startNode.col + 1 < GameGrid.MAX_COL)
        {
            GridNode nodeCheck = gameGrid.grid[startNode.row, startNode.col + 1];
            gameGrid.CheckAndAdd(ref res, nodeCheck);
        }
        
        block.UpdatePosition(res);
    }

}
