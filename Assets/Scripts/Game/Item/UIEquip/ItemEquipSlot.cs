using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipSlot : MonoBehaviour
{
    private static Dictionary<ItemType, ItemEquipSlot> slotsEquip = new Dictionary<ItemType, ItemEquipSlot>();
    [Header("Configs")]
    [SerializeField] ItemType type;
    [SerializeField] Image image;
    [SerializeField] Image imageEmpty;
    [SerializeField] Image fakeShadow;

    [Header("Fill Amount")] 
    [SerializeField] private Image fillAmout;
    [SerializeField] private FillEquipDuration fillColorDuration;
    
    
    private void OnEnable()
    {
        fakeShadow.gameObject.SetActive(false);
        RegisterMe();
    }

    private void OnDisable()
    {
        UnRegisterMe();
    }

    void Equip(Sprite sprItem)
    {
        imageEmpty.gameObject.SetActive(false);
        image.gameObject.SetActive(true);
        
        image.sprite = sprItem;
        fakeShadow.gameObject.SetActive(true);
    }

    void UnEquip()
    {
        imageEmpty.gameObject.SetActive(true);
        image.gameObject.SetActive(false);
        fakeShadow.gameObject.SetActive(false);
    }

    void DoFill(float fillVal)
    {
        fillAmout.DOFillAmount(fillVal, 0.2f);
    }

    void Shake()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(image.transform.DOShakeRotation(0.2f, 20f,10,0))
            .Join(image.transform.DOShakeScale(0.2f, 0.3f,10,0));
    }

    
    //-------------------Manager handler-------------------

    void RegisterMe()
    {
        if (!slotsEquip.ContainsKey(type))
        {
            slotsEquip.Add(type,this);
        }
    }
    
    void UnRegisterMe()
    {
        if(slotsEquip.ContainsKey(type))
        {
            slotsEquip.Remove(type);
        }
    }

    public static void DoFill(ItemType Itemtype,float fillAmount)
    {
        var slotEquip = slotsEquip.FirstOrDefault(x => x.Key == Itemtype);
        if (!slotEquip.Equals(default))
        {
            slotEquip.Value.DoFill(fillAmount);
            slotEquip.Value.Shake();
        }
    }
    
    public static void Equip(ItemType Itemtype,Sprite itemSpr)
    {
        var slotEquip = slotsEquip.FirstOrDefault(x => x.Key == Itemtype);
        if (!slotEquip.Equals(default))
        {
            slotEquip.Value.Equip(itemSpr);
        }
    }
    
    public static void UnEquip(ItemType Itemtype)
    {
        var slotEquip = slotsEquip.FirstOrDefault(x => x.Key == Itemtype);
        if (!slotEquip.Equals(default))
        {
            slotEquip.Value.UnEquip();
        }
    }

    
}

