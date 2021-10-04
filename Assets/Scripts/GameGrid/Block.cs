using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BlockType
{
    Block_1x1,
    Block_1x2,
    Block_2x1,
    Block_1x3,
    Block_2x2
}
public class Block : MonoBehaviour, IPointerClickHandler
{
    public RectTransform rectTrans;
    public List<GridNode> gridContain;
    public int width;
    public ReupdateBlock reupdateBlock;

    [SerializeField] private int _moveDown;
    public void SetContainPlace()
    {
        foreach (var node in gridContain)
        {
            node.isContainObject = true;
        }
    }

    void MoveDown()
    {
        CheckUnderIsBlank();
        reupdateBlock.ReUpdate2X2(this,gridContain[0],_moveDown);
    }


    void CheckUnderIsBlank()
    {
        GridNode nodeStart = gridContain[0];
        GameGrid gameGrid = GameGrid.Instance;
        
        int blankBlock = 0;
        int curRow = nodeStart.row;
        int curCol = nodeStart.col;
        
        int restRow = GameGrid.MAX_ROW - curRow;
        for (int i = 1; i < restRow; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (gameGrid.grid[curRow + i, curCol + j] != null)
                {
                    if (!gameGrid.grid[curRow + i, curCol + j].isContainObject)
                    {
                        blankBlock++;
                    }
                    else
                    {
                        _moveDown = blankBlock / width;
                        return;
                    }
                }
            }
        }
        _moveDown = blankBlock / width;
    }

    public void ResetPosition()
    {
        foreach (var node in gridContain)
        {
            node.isContainObject = false;
        }
        gridContain.Clear();
    }

    public void UpdatePosition(List<GridNode> gridNode)
    {
        foreach (var node in gridNode)
        {
            node.isContainObject = true;
        }

        gridContain = gridNode;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        MoveDown();
    }
}
