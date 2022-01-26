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

    public static string fillGreenColor = "#4BF82E";
    public static string fillYellowColor = "#F8C52E";
    public static string fillRedColor = "#F82E35";

    [Header("FX")] 
    [SerializeField] private ParticleSystem fx_SmokeBreak;
    
    
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
        fx_SmokeBreak.Play();
        imageEmpty.gameObject.SetActive(true);
        image.gameObject.SetActive(false);
        fakeShadow.gameObject.SetActive(false);
    }

    void DoFill(float fillVal)
    {
        fillAmout.DOFillAmount(fillVal, 0.2f);

        Color color = fillAmout.color;
        if (fillVal < 0.3f)
        
            ColorUtility.TryParseHtmlString(fillRedColor, out color);
        else if (fillVal < 0.7f)
            ColorUtility.TryParseHtmlString(fillYellowColor, out color);
        else
            ColorUtility.TryParseHtmlString(fillGreenColor, out color);

        fillAmout.color = color;
    }

    void Shake(bool isEquip = false)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(image.transform.DOShakeRotation(0.2f, 30f, 10, 0))
            .Join(image.transform.DOShakeScale(0.2f, 0.3f, 10, 0));
            
        if(!isEquip)    
            seq.Join(image.DOColor(new Color(1,0.3F,0.3f,1),0.3f).SetLoops(2,LoopType.Yoyo));
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

    public static void DoFill(ItemType Itemtype,float fillAmount, bool isEquip = false)
    {
        var slotEquip = slotsEquip.FirstOrDefault(x => x.Key == Itemtype);
        if (!slotEquip.Equals(default))
        {
            slotEquip.Value.DoFill(fillAmount);
            slotEquip.Value.Shake(isEquip);
        }
    }
    
    public static void Equip(ItemType Itemtype,Sprite itemSpr)
    {
        var slotEquip = slotsEquip.FirstOrDefault(x => x.Key == Itemtype);
        if (!slotEquip.Equals(default))
        {
            SoundManager.Instance.Play("Equip", AudioType.FX);
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

