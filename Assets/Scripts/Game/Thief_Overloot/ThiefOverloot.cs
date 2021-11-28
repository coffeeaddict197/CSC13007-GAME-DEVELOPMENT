using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefOverloot : MonoBehaviour
{
    [Header("Carousel")] 
    [SerializeField] private ThiefCarousel _carousel;

    private void Start()
    {
        _carousel.Scroll(StartLoot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartLoot();
        }
    }
    
    public void StartLoot()
    {
        int rowStartLoot = _carousel.GetCurrentRow();
        List<GridNode> lootNode = GameGrid.Instance.GetAllNodeFromRow(rowStartLoot);
        var listBlock = BlockManager.Instance.BlockCointainGrid(lootNode);
        foreach (var block in listBlock)
        {
            Destroy(block.gameObject);
        }
    }
}