using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class BlockItem : MonoBehaviour
{
    [Header("Configs")] 
    public ItemAssets config;
    
    [Header("UI")] 
    [SerializeField] private Image itemImg;

    [Header("Effect")] 
    [SerializeField] private UIEffect effect;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        effect = GetComponent<UIEffect>();
    }
#endif
    
    

    public void InitItem(BlockType blockType,string itemName = "")
    {
        config = ItemAssetConfigs.Instance.GetAsset(blockType);
        if(config!=null)
        {
            itemImg.sprite = config.GetItemConfigByLevel(1).spr;
        }
    }

    public void OnChangeLevel(int level)
    {
        //Ignore first game not init config yet
        if (config != null )
        {
            itemImg.sprite = config.GetItemConfigByLevel(level).spr;
        }
    }

    public bool OnClick(BaseBlock block)
    {
        return config.handler.OnEquipItem(block);
    }

    public void DoEffect()
    {
        StartCoroutine(DOFX());  //Need to use Coroutine
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.3f, 0.4F).From(1))
            .Append(transform.DOScale(1, 0.4F));
    }

    IEnumerator DOFX()
    {
        effect.enabled = true;
        while (effect.colorFactor < 0.5f)
        {
            effect.colorFactor += Time.deltaTime;
            yield return null;
        }
        
        while (effect.colorFactor > 0)
        {
            effect.colorFactor -= Time.deltaTime;
            yield return null;
        }

        effect.enabled = false;
    }


    public Sprite GetItemSprite() => itemImg.sprite;
    
}





