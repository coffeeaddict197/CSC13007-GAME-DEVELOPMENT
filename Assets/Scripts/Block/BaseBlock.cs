using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseBlock : MonoBehaviour, IPointerClickHandler
{
    [Header("Block data")] 
    [SerializeField] protected BlockData blockData;
    
    [Header("Block config")] 
    [SerializeField] protected List<GridNode> gridContains;
    [SerializeField] protected RectTransform rect;
    [SerializeField] protected int width;
    
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
        Vector2 pos = GetAveragePoint(gridContains);
        rect.anchoredPosition = pos;
        
    }

    protected void UpdatePosition()
    {
        ReUpdateGrid();
        UpdateGridCointain();
        Vector2 newPos = GetAveragePoint(gridContains);
        rect.DOAnchorPos(newPos, 0.5f);
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
        gridContains.Clear();
    }

    public void UpdateGridCointain()
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
    
    Vector2 GetAveragePoint(List<GridNode> listNode)
    {
        Vector2 anchorPoint = Vector2.zero;
        foreach (var node in listNode)
        {
            anchorPoint.x += node.rect.anchoredPosition.x;
            anchorPoint.y += node.rect.anchoredPosition.y;
        }
        anchorPoint /= listNode.Count;
        return anchorPoint;
    }


    public abstract bool CheckCanSpawnAt(GridNode node, GridNode[,] gameGrid, out List<GridNode> listNode);
    public abstract void ReUpdateGrid();
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("1");
        UpdatePosition();
    }
}
