using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BaseBlock))]
public class BlockController : MonoBehaviour , IDropHandler,IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Reference")] 
    [SerializeField] private BaseBlock block;
    [SerializeField] private Image image;

    private static Action onBlockChange;

    private void OnEnable()
    {
        onBlockChange += this.OnBlockChange;
    }

    private void OnDisable()
    {
        onBlockChange -= this.OnBlockChange;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        block = GetComponent<BaseBlock>();
        image = GetComponent<Image>();
    }
#endif
    
    public void OnPointerClick(PointerEventData eventData)
    {
        block.BlockFalling();
    }
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        block.ResetGridContain();
        this.SetSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        block.rect.anchoredPosition += eventData.delta / GameGrid.Instance.canvasContainGrid.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 relativePos = block.FirstNode.rect.anchoredPosition - block.GetPosition();
        Vector2 posOfFirstNode = block.rect.anchoredPosition + relativePos;
        var gridNode = GameGrid.Instance.MinNodeDistance(posOfFirstNode);
        block.UpdatePosition(gridNode);
        StartCoroutine(OnBlockFalling());
        
        image.raycastTarget = true;

    }
    
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            BaseBlock dragObject = eventData.pointerDrag.GetComponent<BaseBlock>();
            
            if (dragObject == block) return;
            
            if (dragObject.blockItem.config.itemName == block.blockItem.config.itemName)
            {
                Debug.Log(dragObject.transform.name + " And " + this.transform.name);
                block.blockData.BlockLevel++;
                Destroy(dragObject.gameObject);
            }
        }
    }


    void OnBlockChange()
    {
        block.BlockFalling();
    }

    void SetSibling()
    {
        int childCount = transform.parent.childCount;
        this.transform.SetSiblingIndex(childCount-1);
    }


    IEnumerator OnBlockFalling()
    {
        for (int i = 0; i <= 3; i++)
        {
            onBlockChange?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }

    }

}
