using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseBlock : MonoBehaviour
{
    [Header("Block data")] 
    [SerializeField] protected BlockData blockData;
    
    [Header("Block config")] 
    public RectTransform rect;
    [SerializeField] protected List<GridNode> gridContains;
    [SerializeField] protected int width;

    
    /// <summary>
    /// Node define first point to draw block
    /// </summary>
    public GridNode FirstNode
    {
        get => gridContains[0];
    }
    

#if UNITY_EDITOR

    private void OnValidate()
    {
        rect = GetComponent<RectTransform>();
    }

#endif

    
    /// <summary>
    /// Setup grid contain block
    /// </summary>
    /// <param name="gridContains"></param>
    public void InitPosition(List<GridNode> gridContains)
    {
        this.gridContains = gridContains;
        foreach (var node in this.gridContains)
        {
            node.isContainObject = true;
        }
        //Set position
        Vector2 pos = GetPosition();
        rect.anchoredPosition = pos;
    }

    public void BlockFalling()
    {
        if(Falling())
        {
            Vector2 newPos = GetPosition();
            rect.DOAnchorPos(newPos, 0.5f);
        }
    }

    public void UpdatePosition(GridNode newFirstNode)
    {
        List<GridNode> newGrid = new List<GridNode>();
        bool canSpawn = CheckCanSpawnAt(newFirstNode, GameGrid.Instance.grid, out newGrid);
        if (canSpawn)
        {
            this.InitPosition(newGrid);
        }
        else
        {
            this.InitPosition(this.gridContains);
        }
        
    }

    public Vector2 GetPosition()
    {
        return Ultility.GetAveragePoint(gridContains);
    }
    
    /// <summary>
    /// Reset grid of block
    /// </summary>
    public void ResetGridContain()
    {
        foreach (var node in gridContains)
        {
            node.isContainObject = false;
        }
    }

    void UpdateGridCointain()
    {
        foreach (var node in gridContains)
        {
            node.isContainObject = true;
        }
    }

    /// <summary>
    /// Return number of step under blank
    /// </summary>
    protected int GetDownStep()
    {
        GridNode nodeStart = gridContains[0];
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
                        return blankBlock / width;
                    }
                }
            }
        }
        return blankBlock / width;
    }
    bool Falling()
    {
        //Setup
        int downStep = GetDownStep();
        if (downStep <= 0)
            return false;
        
        GridNode oldStartNode = gridContains[0];
        GridNode newStartNode = GameGrid.Instance.grid[oldStartNode.row + downStep , oldStartNode.col];
        List<GridNode> newGrid = new List<GridNode>();
        this.ResetGridContain();
        CheckCanSpawnAt(newStartNode, GameGrid.Instance.grid, out newGrid);
        gridContains = newGrid;
        UpdateGridCointain();
        return true;
    }

    public abstract bool CheckCanSpawnAt(GridNode node, GridNode[,] gameGrid, out List<GridNode> listNode);
    
}
