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

    [Header("Game grid setup")] [SerializeField]
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
            rect.anchoredPosition = new Vector2(rootPosition.anchoredPosition.x + col * 240,
                rootPosition.anchoredPosition.y - row * 240);
            grid[row, col].rect = rect;
            grid[row, col].row = row;
            grid[row, col].col = col;
            grid[row, col].isContainObject = false;
            listGrid.Add(grid[row,col]);
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnRandomBlockAt(grid[1, 0]);

        }
    }

    void SpawnRandomBlockAt(GridNode startNode)
    {
        Dictionary<BlockType, List<GridNode>> blockCanSpawn = new Dictionary<BlockType, List<GridNode>>();
        if (startNode != null && !startNode.isContainObject)
        {
            List<GridNode> gridNode = null;
            gridNode = CheckHorizontal(startNode, 2);
            if (gridNode.Count >= 2)
            {
                blockCanSpawn.Add(BlockType.Block_2x1,CheckHorizontal(startNode, 2));
            }

            gridNode = CheckHorizontal(startNode,1);
            if (gridNode.Count >= 1)
            {
                blockCanSpawn.Add(BlockType.Block_1x1,CheckHorizontal(startNode,1));
            }

            gridNode = CheckVerticle(startNode, 2);
            if (gridNode.Count >= 2)
            {
                blockCanSpawn.Add(BlockType.Block_1x2,CheckVerticle(startNode, 2));
            }

            gridNode = CheckVerticle(startNode, 3);
            if (gridNode.Count >= 3)
            {
                blockCanSpawn.Add(BlockType.Block_1x3,CheckVerticle(startNode, 3));
            }

            gridNode = CheckSquare(startNode);
            if (gridNode.Count >= 4)
            {
                blockCanSpawn.Add(BlockType.Block_2x2,CheckSquare(startNode));
            }
        }

        if (blockCanSpawn.Count > 0)
        {
            // int rd = UnityEngine.Random.Range(0, blockCanSpawn.Count);
            // var typeSpawn = blockCanSpawn.ElementAt(rd).Key;
            // Block newSpawn = spawner.SpawnAt(typeSpawn, (blockCanSpawn[typeSpawn]));
            //Block newSpawn = spawner.SpawnAt(BlockType.Block_2x2, (blockCanSpawn[BlockType.Block_2x2]));
        }
    }

    
    List<GridNode> CheckVerticle(GridNode node, int step)
    {
        List<GridNode> res = new List<GridNode>();
        for (int i = 0; i < step; i++)
        {
            if (node.row - i >= 0)
            {
                GridNode nodeCheck = grid[node.row - i, node.col];
                CheckAndAdd(ref res, nodeCheck);
            }
        }
        return res;
    }

    List<GridNode> CheckHorizontal(GridNode node, int step)
    {
        List<GridNode> res = new List<GridNode>();
        for (int i = 0; i < step; i++)
        {
            if (node.col + i < MAX_COL)
            {
                GridNode nodeCheck = grid[node.row, node.col + i];
                CheckAndAdd(ref res, nodeCheck);
            }
        }
        return res;
    }

    List<GridNode> CheckSquare(GridNode node)
    {
        List<GridNode> res = new List<GridNode>();
        CheckAndAdd(ref res, node);
        if (node.row - 1 >= 0)
        {
            GridNode nodeCheck = grid[node.row - 1, node.col];
            CheckAndAdd(ref res, nodeCheck);
        }

        if (node.row - 1 >= 0 && node.col + 1 < MAX_COL)
        {
            GridNode nodeCheck = grid[node.row - 1, node.col + 1];
            CheckAndAdd(ref res, nodeCheck);
        }

        if (node.col + 1 < MAX_COL)
        {
            GridNode nodeCheck = grid[node.row, node.col + 1];
            CheckAndAdd(ref res, nodeCheck);
        }

        if (res.Count >= 4)
        {
            return res;
        }

        return new List<GridNode>();
    }


    public void CheckAndAdd(ref List<GridNode> res, GridNode node)
    {
        if (!node.isContainObject)
        {
            res.Add(node);
        }
    }

    public GridNode MinNodeDistance(Vector2 anchorPos)
    {
        return listGrid.OrderBy(x => Vector2.Distance(x.rect.anchoredPosition, anchorPos)).FirstOrDefault();
    }
    

}