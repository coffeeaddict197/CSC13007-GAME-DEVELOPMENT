﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;


public class BlockManager : MonoSingleton<BlockManager>
{

    public List<BaseBlock> listBlock;
    public List<BaseBlock> blockSpawned;

    [Header("Reference")] 
    [SerializeField] private GameGrid game;
    
    


    public static Action onFullSpawnEvent; //In other word is Overloot event
    
    private void Start()
    {
        InitGame();

    }

    public async void InitGame()
    {
        await UniTask.WaitUntil(() => StartBattle.battleReady);
        blockSpawned = new List<BaseBlock>();
        StartCoroutine(StartSpawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        int numberOfSpawn = 4;
        List<int> hasBeenSpawned = new List<int>();
        for (int i = 0; i < numberOfSpawn; i++)
        {
            List<GridNode> listNode;
            int rdIndexItem = UnityEngine.Random.Range(0, listBlock.Count);
            BaseBlock blockSpawn = listBlock[rdIndexItem];
            if (blockSpawn.CheckCanSpawnAt(game.grid[blockSpawn.heigh-1, UnityEngine.Random.Range(0, GameGrid.MAX_COL-blockSpawn.width)], game.grid, out listNode))
            {
                BaseBlock newBlock = Instantiate(blockSpawn, game.transform);
                RandomLevel(newBlock);
                Vector2 pos = newBlock.InitPosition(listNode);
                newBlock.rect.anchoredPosition = new Vector2(newBlock.rect.anchoredPosition.x, 2500);
                newBlock.rect.DOAnchorPosY(pos.y, 0.5f);
                newBlock.BlockFalling(true);
                yield return new WaitForSeconds(0.1f);
                blockSpawned.Add(newBlock);
                this.CheckEventLooter();
                hasBeenSpawned.Clear();
            }
            else
            {
                if (!hasBeenSpawned.Contains(rdIndexItem))
                {
                    hasBeenSpawned.Add(rdIndexItem);
                }
                
                //FullItem Event
                if (hasBeenSpawned.Count == listBlock.Count)
                {
                    SpawnToHandleOverlootEvent();
                    yield break;
                }
                
                i--;
            }

            yield return null;
        }
    }

    void RandomLevel(BaseBlock blockSpawn)
    {
        int rd = UnityEngine.Random.Range(0, 100);
        if (rd < 10)
        {
            blockSpawn.blockData.BlockLevel += 2;
        }
        else if (rd < 30)
        {
            blockSpawn.blockData.BlockLevel++;
        }
        
    }

    public void Remove(BaseBlock block)
    {
        if (blockSpawned.Contains(block))
        {
            blockSpawned.Remove(block);
        }
    }

    public List<BaseBlock> BlockCointainGrid(List<GridNode> grids)
    {
        List<BaseBlock> res = new List<BaseBlock>();
        foreach (var block in blockSpawned)
        {
            if (!res.Contains(block))
            {
                foreach (var node in grids)
                {
                    if (block.IsCointainNode(node))
                    {
                        res.Add(block);
                    }
                }
            }
        }
        return res;
    }

    void SpawnToHandleOverlootEvent()
    {
        OverlootEvent.Instance.isPlayingEvent = true;
        BaseBlock model = listBlock[2];
        //Just spawn one and occur event OVERLOOT
        BaseBlock newBlock = Instantiate(model, game.transform);
        RandomLevel(newBlock);
        newBlock.rect.anchoredPosition = new Vector2(newBlock.rect.anchoredPosition.x, 2500);
        newBlock.rect.DOAnchorPosY(960, 0.5f).OnComplete(() =>
        {
            OverlootEvent.Instance.SpawnThief();
            onFullSpawnEvent?.Invoke();
            Sequence seq = DOTween.Sequence();
            seq.Join(newBlock.transform.DOJump(new Vector3(2, -8f, 0), 7F, 1, 2.5f))
                .Join(newBlock.transform.DORotate(new Vector3(0, 0, 360F), 0.6f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear).SetLoops(10, LoopType.Incremental))
                .Join(newBlock.transform.DOScale(new Vector3(1.3F, 1.3F, 1.3F), 0.5F).SetDelay(0.3F).From(Vector3.one).OnComplete(
                    () =>
                    {
                        OverlootEvent.Instance.OnNotifyOverlootEvent();
                    }));
        });
    }


    public bool CheckFullGrid()
    {
        var obj = blockSpawned.FindAll(x => x.listNodeContain.Find(n => n.isContainObject && n.row <= 1) != null);
        if (obj.Count >= 3)
            return true;
        return false;
    }
    void CheckEventLooter()
    {
        if (CheckFullGrid())
        {
            WarningFullSlot.Instance.DOFX();
        }
    }

}
