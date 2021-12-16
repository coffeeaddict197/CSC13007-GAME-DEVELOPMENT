using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefOverloot : MonoBehaviour
{
    [Header("Carousel")] 
    [SerializeField] private ThiefCarousel _carousel;


    public void ActiveCarousel() => _carousel.gameObject.SetActive(true);

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

    public IEnumerator IELootAction(Action callback)
    {
        callback += StartLoot;
        yield return StartCoroutine(_carousel.Scroll(callback));
        yield return null;
    }

}