using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameGrid : MonoSingleton<GameGrid>
{
    public RectTransform gridLayout;
    public GridNode[,] grid;
    public List<GridNode> listGrid; //ShowInspector
    public Canvas canvasContainGrid;
    //public SpawnStrategy spawner;

    public const int MAX_COL = 8;
    public const int MAX_ROW = 7;
    public const int NUMBLOCK = MAX_COL * MAX_ROW;
    public const int CEIL_SIZE = 240;

    [Header("Game grid setup")] 
    [SerializeField]
    private GameObject gameGrid;

    [SerializeField] private RectTransform rootPosition;

    protected override void Awake()
    {
        base.Awake();
        grid = new GridNode[MAX_ROW, MAX_COL];
        for (int i = 0; i < NUMBLOCK; i++)
        {
            int row = i / MAX_COL;
            int col = i % MAX_COL;
            grid[row, col] = new GridNode();

            // Vector3Int cellPosition = gridLayout.WorldToCell(grid[row, col].position);
            GameObject obj = Instantiate(rootPosition.gameObject, gameGrid.transform);
            RectTransform rect = obj.transform as RectTransform;
            rect.anchoredPosition = new Vector2(rootPosition.anchoredPosition.x + col * CEIL_SIZE,
                rootPosition.anchoredPosition.y - row * CEIL_SIZE);
            grid[row, col].rect = rect;
            grid[row, col].row = row;
            grid[row, col].col = col;
            grid[row, col].isContainObject = false;
            listGrid.Add(grid[row,col]);
        }
    }
  
    public GridNode MinNodeDistance(Vector2 anchorPos)
    {
        return listGrid.OrderBy(x => Vector2.Distance(x.rect.anchoredPosition, anchorPos)).FirstOrDefault();
    }

    public List<GridNode> GetAllNodeFromRow(int row, int times = 2)
    {
        List<GridNode> res = new List<GridNode>();
        for (int i = row; i < row + times; i++)
        {
            for (int j = 0; j < MAX_COL; j++)
            {
                res.Add(grid[i,j]);
            }
        }
        return res;
    }
    
    

}