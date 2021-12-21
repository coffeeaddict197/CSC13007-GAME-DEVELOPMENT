using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockItemDetail : MonoBehaviour
{
    [Header("Visualize")] 
    [SerializeField] private Image img;
    [SerializeField] PosionHandler config;
    

    private void Start()
    {
        var item = GetComponent<BlockItem>();
        if (item.config != null && item.config.itemType == ItemType.Posion)
        {
            config = (PosionHandler)item.config.handler;
            img.sprite = config.representSpr;
        }
        else
        {
            img.gameObject.SetActive(false);
        }
    }
}
