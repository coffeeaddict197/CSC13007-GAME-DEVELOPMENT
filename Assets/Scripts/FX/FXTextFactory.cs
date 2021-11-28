using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;


[System.Serializable]
public class FXTextFactory : FXPool
{
    [SerializeField] private TextMeshProUGUI textModel;
    [SerializeField] private Ease ease;

    public static Color damageColor = new Color(0.97f, 0.207f, 0.207f, 1f);
    public static Color healthColor = new Color(0.235f, 1f, 0.196f, 1f);

    public void Init()
    {
        Init(textModel);
        CreatePool();
    }

    public void SpawnFX(Vector3 position,string txt, Color textColor)
    {
        var obj = GetObject();
        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.text = txt;
            obj.color = textColor;

            Sequence txtSeq = DOTween.Sequence();
            txtSeq.Append(obj.transform.DOScale(1, 0.3f).From(1.7F).SetEase(ease))
                .Join(obj.transform.DOJump(obj.transform.position + new Vector3(0.1f,-0.1f,0),0.2F,0,0.5f)).SetDelay(0.2F).OnComplete(
                    () =>
                    {
                        obj.gameObject.SetActive(false);
                    });
        }
    }
    
    TextMeshProUGUI GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            var gObj = (TextMeshProUGUI) pool[i];
            if (!gObj.gameObject.activeSelf)
            {
                return gObj;
            }
        }
        return null;
    }
}

