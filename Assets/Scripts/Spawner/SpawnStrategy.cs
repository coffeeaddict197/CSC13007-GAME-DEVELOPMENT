using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;


public class SpawnStrategy : MonoBehaviour
{

    public List<BaseBlock> listBlock;

    [Header("Reference")] 
    [SerializeField] private GameGrid game;


    private void Start()
    {
        StartCoroutine(StartSpawn());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(StartSpawn());

        }
    }

    IEnumerator StartSpawn()
    {
        int numberOfSpawn = 4;
        for (int i = 0; i < numberOfSpawn; i++)
        {
            List<GridNode> listNode;
            BaseBlock blockSpawn = listBlock[UnityEngine.Random.Range(0, listBlock.Count)];
            if (blockSpawn.CheckCanSpawnAt(game.grid[blockSpawn.heigh-1, UnityEngine.Random.Range(0, GameGrid.MAX_COL-blockSpawn.width)], game.grid, out listNode))
            {
                BaseBlock newBlock = Instantiate(blockSpawn, game.transform);
                Vector2 pos = newBlock.InitPosition(listNode);

                newBlock.rect.anchoredPosition = new Vector2(newBlock.rect.anchoredPosition.x, 2500);
                newBlock.rect.DOAnchorPosY(pos.y, 0.5f);
                
                newBlock.BlockFalling();

                yield return new WaitForSeconds(0.1f);
            }


        }
    }
    
    

}
