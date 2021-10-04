using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnStrategy : MonoBehaviour
{

    public List<BaseBlock> listBlock;

    [Header("Reference")] 
    [SerializeField] private GameGrid game;


    private void Start()
    {
        BaseBlock block = listBlock[1];
        List<GridNode> listNode;
        if (block.CheckCanSpawnAt(game.grid[1, 0], game.grid, out listNode))
        {
            BaseBlock newBlock = Instantiate(block, game.transform);
            newBlock.InitPosition(listNode);
        }
    }

    // public Block _1x1;
    // public Block _1x3;
    // public Block _2x1;
    // public Block _2x2;
    // public Block _1x2;
    //
    // public Block SpawnAt(BlockType type, List<GridNode> listNode)
    // {
    //     switch (type)
    //     {
    //         case BlockType.Block_1x2:
    //             return Spawn(_1x2, listNode);
    //         case BlockType.Block_1x3:
    //             return Spawn(_1x3, listNode);
    //         case BlockType.Block_2x1:
    //             return Spawn(_2x1, listNode);
    //         case BlockType.Block_2x2:
    //             return Spawn(_2x2, listNode);
    //         case BlockType.Block_1x1:
    //             return Spawn(_1x1, listNode);
    //     }
    //
    //     return null;
    // }
    //
    // Block Spawn(Block block, List<GridNode> listNode)
    // {
    //     Block newBlock = Instantiate(block, this.transform);
    //     newBlock.rectTrans.anchoredPosition = GetAveragePoint(listNode);
    //     newBlock.gridContain = listNode;
    //     newBlock.SetContainPlace();
    //     return newBlock;
    // }
    //
    //

}
